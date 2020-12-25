using BargainFetcherCrawler.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack.CssSelectors.NetCore;
using HtmlAgilityPack;
using System.IO;
using BargainFetcherCrawler.Services;
using System.Net.Http;

namespace BargainFetcherCrawler.WebshopPages.Emag
{
    public class WebshopMainPageEMAG : AMainPage
    {
        private static readonly HttpClient _client = new HttpClient();
        public WebshopMainPageEMAG(string webshopLink):base(webshopLink)
        {
        }


        protected override List<string[]> LoadCategoryNamesAndCategoryLinks()
        {
            List<string[]> categoryAndNames = new List<string[]>();

            string[] separators = new string[] { "\r\n" };
            foreach (var line in Properties.Resources.LinksCategoriesEMAG.Split(separators, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] resourceLine = line.Split(',');
                resourceLine[1] = resourceLine[1].Trim();
                categoryAndNames.Add(resourceLine);
            }
            return categoryAndNames;
        }

    }
}