namespace BargainFetcherCrawler.Models.DataModels
{
    public class ProductDetail
    {
        public int ProductDetailID { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }

        public override string ToString()
        {
            return $"           ProductDetailID : {ProductDetailID} \n           Category : {Category} \n            Title : {Title}" +
                $"\n            Text : {Text}";
        }
    }
}