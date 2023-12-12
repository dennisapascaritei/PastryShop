namespace PastryShop.Domain.Aggregates.OrderAggregate
{
    public class LineItem
    {
        internal LineItem(Guid orderId, Guid productId, string name, string description, double price, double weight, string imageURL)
        {
            LineItemId = Guid.NewGuid();
            OrderId = orderId;
            ProductId = productId;
            Name = name;
            Description = description;
            Price = price;
            Weight = weight;
            ImageURL = imageURL;
        }
        public Guid LineItemId { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public double Price { get; private set; }
        public double Weight { get; private set; }
        public string ImageURL { get; private set; }

        public static LineItem CreateLineItem(Guid orderId, Guid productId, string name, string description, double price, double weight, string imageURL)
        {
            //To Do: add validation, error handling strategies, error notification strategies

            return new LineItem(orderId, productId, name, description, price, weight, imageURL)
            {
                LineItemId = Guid.NewGuid(),
                OrderId = orderId,
                ProductId = productId,
                Name = name,
                Description = description,
                Price = price,
                Weight = weight,
                ImageURL = imageURL,
            };
        }
        public void UpdateLineItem(Product product)
        {
            ProductId = product.ProductId;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            Weight = product.Weight;
            ImageURL = product.ImageURL;
        }
    }
}