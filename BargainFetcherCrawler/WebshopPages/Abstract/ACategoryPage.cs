using BargainFetcherCrawler.Interfaces;
using BargainFetcherCrawler.Services;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BargainFetcherCrawler.Abstract
{
    public abstract class ACategoryPage
    {
        protected HtmlDocument _html;

        public ACategoryPage(string link)
        {
            LoadMainPageFromLink(link);
        }

        //Crawler service used here to inject the html we have to parse internally
        protected async void LoadMainPageFromLink(string link)
        {
            _html = await Crawler.GetPageAsync(link);

        }
        //Inherited Properties From AMainPage type of class
        public string CategoryName { get; set; }
        public string CategoryLink { get; set; }


        public int NrOfProducts { get => LoadNrOfProducts(); }
        public decimal NrOfPages { get => LoadNrOfPages(); }
        public List<string> Brands { get; set; }
        public List<string> PageLinks { get => LoadPageLinks(); }
        public List<string> ProductsOnSale { get => LoadProductsOnSale(); }

        protected abstract int LoadNrOfProducts();
        protected abstract decimal LoadNrOfPages();
        protected abstract List<string> LoadBrands();
        protected abstract List<string> LoadPageLinks();
        public abstract void LoadCategoryNameAndCategoryLink(string[] CategoryNameAndCategoryLink);
        protected abstract List<string> LoadProductsOnSale();
    }
}
