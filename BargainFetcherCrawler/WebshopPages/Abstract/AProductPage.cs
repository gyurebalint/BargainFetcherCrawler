using BargainFetcherCrawler.Models.DataModels;
using BargainFetcherCrawler.Services;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace BargainFetcherCrawler.Abstract
{
    public abstract class AProductPage
    {
        //Inherited Property from a ACategoryPage type of class
        public string ProductPageLink;
        public string CategoryName;
        public string CategoryLink;
        public string WebhsopName;
        public Product Produt { get=>LoadProduct(); }

        protected HtmlDocument _htmlDoc;
        public AProductPage(string link)
        {
            LoadMainPageFromLink(link);
            ProductPageLink = link;
        }

        //Crawler service used here to inject the html we have to parse internally
        private async void LoadMainPageFromLink(string link)
        {
            _htmlDoc = await Crawler.GetPageAsync(link);
        }

        public abstract Product LoadProduct();

    }
}
