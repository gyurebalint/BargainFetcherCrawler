using BargainFetcherCrawler;
using BargainFetcherCrawler.Abstract;
using BargainFetcherCrawler.Services;
using System.Collections.Generic;
using System.Text;
using System;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using HtmlAgilityPack.CssSelectors.NetCore;
using System.Linq;
using System.Threading.Tasks;

namespace Webshops.WebshopPages.Emag
{
    public class CategoryPageEMAG : ACategoryPage
    {
        public CategoryPageEMAG(string link) : base(link)
        {
        }
        public override void LoadCategoryNameAndCategoryLink(string[] categoryNameAndCategoryLink)
        {
            this.CategoryName = categoryNameAndCategoryLink[0];
            this.CategoryLink = categoryNameAndCategoryLink[1];
        }
        protected override List<string> LoadBrands()
        {
            List<string> brands = new List<string>();
            IEnumerable<HtmlNode> manufacturers;
            var IsManuf = _html.DocumentNode.QuerySelector(".filter-body.filter-min-fixed.js-scrollable").Descendants().Any();
            if (IsManuf)
            {
                manufacturers = _html.DocumentNode.QuerySelector(".filter-body.filter-min-fixed.js-scrollable").Descendants();
            }
            else
            {
                manufacturers = _html.DocumentNode.QuerySelector(".filter-body.js-scrollable").Descendants();
            }

            foreach (var manuf in manufacturers)
            {
                string brand = Regex.Replace(manuf.InnerText, "[0-9()]", "");
                if (brand != string.Empty)
                {
                    brand = brand.ToLower().Trim();
                    if (brand == "starlight")
                    {
                        brand = "star-light";
                    }
                    //if (brand.Contains(" "))
                    //{
                    //    brand.Replace(" ", "-");
                    //}
                    brands.Add(brand);
                }
            }
            return brands;
        }
        protected override decimal LoadNrOfPages()
        {
            return Math.Ceiling((decimal)this.NrOfProducts / 60);
        }
        protected override int LoadNrOfProducts()
        {
            string numberOfProducts = _html.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[3]/div[2]/div[1]/section[1]/div[1]/div[2]/div[1]/div[3]/div[2]/div[2]/div[1]/h1[1]/span[2]").InnerText;
            int numberOfProductsInt = Int32.Parse(Regex.Replace(numberOfProducts, "[^0-9]", ""));

            return numberOfProductsInt;
        }
        protected override List<string> LoadPageLinks()
        {
            List<string> categoryLinksByBrand = LinkBuilder.GetCategoryLinksByBrand(this.CategoryLink, (int)NrOfPages);

            return categoryLinksByBrand;
        }
        protected override List<string> LoadProductsOnSale()
        {
            List<string> links = new List<string>();
            foreach (var pageLink in this.PageLinks)
            {
                //var blas = htmlDoc.DocumentNode.QuerySelectorAll(".product-old-price");
                var cards = _html.DocumentNode.QuerySelectorAll(".card-section-wrapper.js-section-wrapper");

                foreach (var card in cards)
                {
                    string sale = card.SelectSingleNode(".//p[@class = 'product-old-price']").InnerText;

                    if (sale != string.Empty)
                    {
                        var link = card.SelectSingleNode(".//div[@class='card-heading']/a").GetAttributeValue("href", "Link Not Found");
                        if (link != "Link Not Found")
                        {
                            links.Add(link);
                        }
                    }
                }
            }
            return links;
        }
    }
}
