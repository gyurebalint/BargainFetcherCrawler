using BargainFetcherCrawler.Abstract;
using BargainFetcherCrawler.Models.DataModels;
using BargainFetcherCrawler.Services;
using BargainFetcherCrawler.WebshopPages.Emag;
using HtmlAgilityPack;
using System;
using System.Threading.Tasks;
using Webshops.WebshopPages.Emag;

namespace BargainFetcherCrawler
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //HtmlDocument doc = await Crawler.GetPageAsync("https://www.emag.hu/homepage");
            //Load Main page
            AMainPage mainPageEMAG = new WebshopMainPageEMAG("https://www.emag.hu/homepage");

            //Mosógépek
            foreach (var item in mainPageEMAG.CategoryNamesAndCategoryLinks)
            {
                //Console.WriteLine(item[0]);
                //Console.WriteLine(item[1]);

                //First page in category
                ACategoryPage categoryPageEMAG = new CategoryPageEMAG(item[1]);
                categoryPageEMAG.LoadCategoryNameAndCategoryLink(item);

                foreach (var productPageLink in categoryPageEMAG.ProductsOnSale)
                {
                    ProductPageEMAG productPageEMAG = new ProductPageEMAG(productPageLink);
                    productPageEMAG.CategoryName = categoryPageEMAG.CategoryName;
                    productPageEMAG.CategoryLink = categoryPageEMAG.CategoryLink;

                    Product product = productPageEMAG.LoadProduct();

                    await Crawler.PostProductAsync(product);
                }
            }
            Console.ReadLine();
            //Inside, Main page will load all the categories and their links.
            //Have to loop through category links

            //foreach (var item in eMAG.CategoryNamesAndCategoryLinks)
            //{
            //    HtmlAgilityPack.HtmlDocument categoryPage = await crawler.GetPageAsync(item[1]);
            //    ACategoryPage emagCategory = new CategoryPageEMAG(categoryPage);

            //    emagCategory.CategoryLink = item[0];
            //    emagCategory.CategoryName = item[1];

            //    // "https://www.emag.hu/mosogepek/brand/beko/c1"
            //    foreach (var categoryLink in emagCategory.PageLinks)
            //    {
            //        HtmlAgilityPack.HtmlDocument productDocument = await crawler.GetPageAsync(categoryLink);

            //        ProductPageEMAG productPageEMAG = new ProductPageEMAG(productDocument);

            //        Product product = productPageEMAG.GetProduct();
            //        crawler.PostProduct(product);
            //    } 
            //}
        }
    }
}
