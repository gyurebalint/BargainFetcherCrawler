namespace BargainFetcherCrawler.Models.DataModels
{
    public class ProductReview
    {
        public int ProductReviewID { get; set; }
        public string Name { get; set; }
        public string DateOfReview { get; set; }
        public string ProductReviews { get; set; }
        public string Title { get; set; }
        public int Stars { get; set; }
        public string Text { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }

        public override string ToString()
        {
            return $"   ProductReviewID : {ProductReviewID} \n   Name : {Name} \n   Date of review : {DateOfReview}" +
                $"\n    Title : {Title} \n   Text : {Text} \n   Stars : {Stars}";
        }
    }
}