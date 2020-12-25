using BargainFetcherCrawler.Abstract;
using BargainFetcherCrawler.Models.DataModels;
using HtmlAgilityPack.CssSelectors.NetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BargainFetcherCrawler.WebshopPages.Emag
{
    public class ProductPageEMAG : AProductPage
    {
        public ProductPageEMAG(string link) : base(link)
        {
        }
        private Product Product;
        public override Product LoadProduct()
        {
            try
            {
                string productCode = this._htmlDoc.DocumentNode.QuerySelector(".product-code-display.pull-left").InnerText.Replace("Termékkód: ", "").Trim();
                string title = _htmlDoc.DocumentNode.QuerySelector(".page-title").InnerText.ToLower().Trim();
                string brand = "";  //IMPLEMENT THIS
                string model = ""; //IMPLEMENT THIS
                int oldPrice = Convert.ToInt32(_htmlDoc.DocumentNode
                    .SelectSingleNode(".//p[@class = 'product-old-price']/s").InnerText.Replace("\n", "").Replace("&#46;", "").Replace(" Ft", "").Trim());

                int newPrice = Convert.ToInt32(_htmlDoc.DocumentNode
                    .SelectSingleNode(".//p[@class = 'product-new-price']").InnerText.Replace("\n", "").Replace("&#46;", "").Replace(" Ft", "").Trim());

                int sale = Convert.ToInt32(Regex.Replace(_htmlDoc.DocumentNode.SelectSingleNode(".//span[@class = 'product-this-deal']").InnerText, "[^0-9]", ""));
                int nrOfReviews = LoadNrOfReviewsPropertyOfProduct(_htmlDoc);
                double starsAverage = LoadStarsAveragePropertyOfProduct(_htmlDoc);

                List<ProductDetail> productDetails = GetProductDetails();
                ProductDescription productDescription = GetProductDescription();
                List<ProductReview> productReviews = GetProductReviews();
                List<ProductImage> productImages = GetProductImages();
                //Console.WriteLine(Product.ToString());

                Product = new Product(productCode,webshopName:"EMAG", title, brand, model, oldPrice, newPrice, sale, nrOfReviews, starsAverage, this.ProductPageLink, productDescription,
                    productDetails, productReviews, productImages, this.CategoryName, this.CategoryLink);

                return Product;
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("Ran into an error while creating the product");
            }
        }
        private int LoadNrOfReviewsPropertyOfProduct(HtmlAgilityPack.HtmlDocument htmlDoc)
        {
            string Temp = "";
            Temp = _htmlDoc.DocumentNode.SelectSingleNode(".//p[@class = 'hidden-xs']/a").InnerText;
            Temp = Regex.Replace(Temp, "[^0-9]", "");
            if (!string.IsNullOrEmpty(Temp) || !string.IsNullOrWhiteSpace(Temp))
            {
                return Convert.ToInt32(Temp);
            }
            //0 is an error since we only gather products which is on sale
            return 0;
        }
        private double LoadStarsAveragePropertyOfProduct(HtmlAgilityPack.HtmlDocument htmlDoc)
        {
            string Temp = "";
            if (_htmlDoc.DocumentNode.SelectSingleNode(".//p[@class = 'review-rating-data']") != null)
            {
                Temp = _htmlDoc.DocumentNode.SelectSingleNode(".//p[@class = 'review-rating-data']").InnerText;
                if (!string.IsNullOrEmpty(Temp) || !string.IsNullOrWhiteSpace(Temp))
                {
                    return (Convert.ToDouble(Temp));
                }
            }
            return 0;
        }
        private List<ProductImage> GetProductImages()
        {
            try
            {
                List<ProductImage> productImages = new List<ProductImage>();
                var nodes = _htmlDoc.DocumentNode.SelectNodes("//div[@id='product-gallery']//a");
                foreach (var image in nodes)
                {
                    ProductImage productImage = new ProductImage();
                    productImage.ImageURI = image.GetAttributeValue("href", "Not found");
                    productImages.Add(productImage);
                    //Console.WriteLine(productImage.ToString());
                }
                return productImages;
            }
            catch
            {

                throw new ArgumentException("Ran into an error while creating the product images");
            }

        }
        private List<ProductReview> GetProductReviews()
        {
            try
            {
                List<ProductReview> reviews = new List<ProductReview>();

                var areThereReviews = _htmlDoc.DocumentNode.QuerySelectorAll(".product-review-item.js-review-item.row").Any();
                if (areThereReviews)
                {
                    var reviewNode = _htmlDoc.DocumentNode.QuerySelectorAll(".product-review-item.js-review-item.row");
                    foreach (var item in reviewNode)
                    {
                        ProductReview productReview = new ProductReview();
                        productReview.Name = item.QuerySelector(".product-review-author.mrg-top-xs.semibold").InnerText;
                        productReview.DateOfReview = item.QuerySelector(".small.text-muted.mrg-sep-none").InnerText;
                        productReview.Title = item.SelectSingleNode(".//h3[@class ='product-review-title']/a").InnerText;
                        productReview.Text = item.QuerySelector(".mrg-btm-xs.js-review-body.review-body-container").InnerText;
                        //var reviewStar = item.SelectSingleNode(".//div[@class ='star-rating-inner']").OuterHtml;
                        var reviewStar = item.QuerySelector(".star-rating-inner").OuterHtml;

                        productReview.Stars = Convert.ToInt32(Regex.Replace(reviewStar, "[^0-9]", ""));

                        //bool isThereReviewStar = item.Descendants("div")
                        //    .Any(d => d.GetAttributeValue("class", "") == "star-rating-inner");
                        //Console.WriteLine(productReview.ToString()); ;
                        reviews.Add(productReview);
                    }
                }
                return reviews;
            }
            catch
            {
                throw new ArgumentException("Ran into an error while creating the product reviews");
            }
        }
        private ProductDescription GetProductDescription()
        {
            try
            {
                ProductDescription productDescription = new ProductDescription();
                string Description = string.Empty;
                int NumberOfPicturesInDescription = 0;
                string[] Images = new string[NumberOfPicturesInDescription];

                var descriptionNode = _htmlDoc.DocumentNode.SelectSingleNode(".//div[@class ='product-page-description-text']");

                foreach (var p in descriptionNode.SelectNodes(".//p"))
                {
                    string pInnerText = p.InnerHtml;
                    if (pInnerText.Contains("\n"))
                    {
                        pInnerText = p.InnerHtml.Replace("\n", "").Trim();
                    }
                    Description += pInnerText;
                }

                return productDescription;
            }
            catch 
            {

                throw new ArgumentException("Ran into an error while creating the product description");
            }

        }
        private List<ProductDetail> GetProductDetails()
        {
            try
            {
                List<ProductDetail> productDetails = new List<ProductDetail>();

                var areThereDetails = _htmlDoc.DocumentNode.QuerySelectorAll(".pad-top-sm").Any();

                if (areThereDetails)
                {
                    var tableNode = _htmlDoc.DocumentNode.SelectNodes(".//div[@class = 'pad-top-sm']");

                    foreach (var node in tableNode)
                    {
                        var category = node.FirstChild.FirstChild.InnerText;

                        foreach (var detail in node.SelectNodes(".//tbody"))
                        {
                            foreach (var tr in detail.SelectNodes(".//tr"))
                            {
                                ProductDetail productDetail = new ProductDetail();
                                productDetail.Category = category;
                                var tds = tr.SelectNodes("td");
                                productDetail.Title = tds[0].InnerText;
                                productDetail.Text = tds[1].InnerText;
                                productDetails.Add(productDetail);
                                //Console.WriteLine(productDetail.ToString());
                            }
                        }
                    }
                }
                return productDetails;
            }
            catch 
            {

                throw new ArgumentException("Ran into an error while creating the product details");
            }

        }

    }
}