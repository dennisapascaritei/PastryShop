namespace PastryShop.Api.Contracts.Products.Response
{
    public class ProductResponse
    {
        public Guid ProductId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public double? Price { get; private set; }
        public double? Weight { get; private set; }
        public string ImageURL { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime LastUpdated { get; private set; }
    }
}
