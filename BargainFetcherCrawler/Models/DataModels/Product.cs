using System;
using System.Collections.Generic;
using System.Text;

namespace BargainFetcherCrawler.Models.DataModels
{
    public class Product
    {
        public Product( string productCode,string webshopName, string title, string brand, string model, 
            int oldPrice, int newPrice, int sale, int numberOfReviews, double starsAverage, string link,
            ProductDescription description, List<ProductDetail> details, List<ProductReview> reviews,
            List<ProductImage> images, string categoryName, string categoryLink)
        {
            ProductCode = productCode;
            WebshopName = webshopName;
            Title = title;
            Brand = brand;
            Model = model;
            OldPrice = oldPrice;
            NewPrice = newPrice;
            Sale = sale;
            NumberOfReviews = numberOfReviews;
            StarsAverage = starsAverage;
            Link = link;
            Description = description;
            Details = details;
            Reviews = reviews;
            Images = images;
            CategoryName = categoryName;
            CategoryLink = categoryLink;
        }
        public Product()
        {

        }

        public int ProductID { get; set; }
        public string ProductCode { get; set; }
        public string WebshopName { get; set; }
        public string Title { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int OldPrice { get; set; }
        public int NewPrice { get; set; }
        public int Sale { get; set; }
        public int NumberOfReviews { get; set; }
        public double StarsAverage { get; set; }
        public string Link { get; set; }
        public ProductDescription Description { get; set; }
        public List<ProductDetail> Details { get; set; }
        public List<ProductReview> Reviews { get; set; }
        public List<ProductImage> Images { get; set; }
        public string CategoryName { get; set; }
        public string CategoryLink { get; set; }

        public override string ToString()
        {
            return $" Product ID : {ProductID}\n Product Code : {ProductCode}\n Brand : {Brand} \n Model : {Model} \n" +
                $" Old Price : {OldPrice} \n New price : {NewPrice} \n Sale : {Sale} \n Number of reviews : {NumberOfReviews} \n" +
                $" Average star reating : {StarsAverage} \n Title : {Title} \n Link : {Link}\n \n";
        }
    }
}
