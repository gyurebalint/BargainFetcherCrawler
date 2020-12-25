using BargainFetcherCrawler.Models;
using BargainFetcherCrawler.Models.DataModels;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using System.Text.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net;

namespace BargainFetcherCrawler.Services
{
    public static class Crawler
    {
        static readonly HttpClient _client = new HttpClient();

        public static async Task<HtmlDocument> GetPageAsync(string pageLink)
        {
            try
            {
                using (var response = _client.GetAsync(pageLink).Result)
                {
                    using (var content = response.Content)
                    {
                        var result = await content.ReadAsStringAsync();
                        HtmlDocument HtmlDocument = new HtmlDocument();

                        HtmlDocument.LoadHtml(result);
                        return HtmlDocument;
                    }
                }
            }
            catch
            {
                throw new ArgumentException("Reading the page was NOT successful!");
            }
        }

        public static async Task PostProductAsync(Product product)
        {
            if (await DoesProductAlreadyExistsAsync(product) != true)
            {
                var json = JsonSerializer.Serialize(product);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = "http://bargainfetcherwebapi.azurewebsites.net/api/products/";
                var response = await _client.PostAsync(url, data);
                //string result = response.Content.ReadAsStringAsync();
                
            }
        }
        private static async Task<bool> DoesProductAlreadyExistsAsync(Product product)
        {
            try
            {
                string searchProductcode = product.ProductCode.Replace("+", "%2b");
                //What does OData return if the query has no results.
                string productQuery = $"http://bargainfetcherwebapi.azurewebsites.net/api/products?$select=productCode&$filter=ProductCode eq '{searchProductcode}'";
                var response = await _client.GetAsync(productQuery);
                if (response.Content.ReadAsStringAsync().Result == "[]")
                {
                    return false;
                }
                return true;
            }
            catch 
            {

                throw new ArgumentException("DoesProductExist function is wrong!");
            }

        }
    }
}
