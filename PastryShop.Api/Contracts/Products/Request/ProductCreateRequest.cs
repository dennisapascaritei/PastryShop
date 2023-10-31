namespace PastryShop.Api.Contracts.Products.Request
{
    public class ProductCreateRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Weight { get; set; }
        public string ImageURL { get; set; }
    }
}
