

namespace BargainFetcherCrawler.Models.DataModels
{
    public class ProductDescription
    {
        public int ProductDescriptionID { get; set; }
        public string Text { get; set; }
        public string[] ImagesURI { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}