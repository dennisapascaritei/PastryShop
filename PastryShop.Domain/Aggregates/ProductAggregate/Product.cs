
using PastryShop.Domain.Aggregates.UserProfileAggregate;

namespace PastryShop.Domain.Aggregates.ProductAggregate
{
    public class Product
    {
        private Product()
        {

        }
        public Guid ProductId { get; private set; }
        public string Name { get; private set;}
        public string Description { get; private set;}
        public double? Price { get; private set;}
        public double? Weight { get;private set;}
        public string ImageURL { get; private set;}
        public DateTime CreatedDate { get; private set; }
        public DateTime LastUpdated { get; private set;}

        public static Product CreateProduct(string name, string description, double price, double weight, string imageURL)
        {
            //To Do: add validation, error handling strategies, error notification strategies

            return new Product()
            {
                Name = name,
                Description = description,
                Price = price,
                Weight = weight,
                ImageURL = imageURL,
                CreatedDate = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow
            };
        }

        public void UpdateProduct(Product product)
        {
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            Weight = product.Weight;
            ImageURL = product.ImageURL;
            CreatedDate = DateTime.UtcNow;
            LastUpdated = DateTime.UtcNow;
        }
    }
}
