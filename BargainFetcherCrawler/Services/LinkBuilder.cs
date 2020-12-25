using System;
using System.Collections.Generic;
using System.Text;

namespace BargainFetcherCrawler.Services
{
    public static class LinkBuilder
    {
        public static List<string> GetCategoryLinksByBrand(string startLink, int numberOfPages)
        {
            List<string> links = new List<string>();

            for (int i = 1; i < numberOfPages + 1; i++)
            {
                string endLink = startLink + "p" + i + "/c";
                links.Add(endLink);
            }

            return links;
        }
    }
}
