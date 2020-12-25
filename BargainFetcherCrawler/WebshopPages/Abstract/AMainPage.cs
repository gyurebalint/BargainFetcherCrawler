using BargainFetcherCrawler.Interfaces;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using BargainFetcherCrawler.Services;

namespace BargainFetcherCrawler.Abstract
{
    public abstract class AMainPage
    {
        protected HtmlDocument _html;

        public AMainPage(string link)
        {
            LoadMainPageFromLink(link);
        }
        //Crawler service used here to inject the html we have to parse internally
        private async void LoadMainPageFromLink(string link)
        {
            _html = await Crawler.GetPageAsync(link);
        }
        public List<string[]> CategoryNamesAndCategoryLinks { get => LoadCategoryNamesAndCategoryLinks(); }

        protected abstract List<string[]> LoadCategoryNamesAndCategoryLinks();
    }
}
