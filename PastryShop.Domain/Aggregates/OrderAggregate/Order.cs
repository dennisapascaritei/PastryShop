
using PastryShop.Domain.Aggregates.UserProfileAggregate;
using System.Diagnostics;
using System.Xml.Linq;

namespace PastryShop.Domain.Aggregates.OrderAggregate
{
    public class Order
    {
        private Order() { }

        public Guid OrderId { get; private set; }
        public Guid UserProfileId { get; private set; }
        public UserProfile UserProfile { get; private set; }
        public IEnumerable<Product> ProductList { get; private set; }
        public double Price { get; private set; }
        public Guid ShipmentTypeId { get; private set; }
        public ShipmentType ShipmentType { get; private set; }
        public ShippingAddressOrder ShippingAddressOrder { get; private set; }
        public string UserInstructions { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime DeliveryDate { get; private set;}

        public static Order CreateOrder(Guid userProfileId, IEnumerable<Product> productList, double price, 
            Guid shipmentTypeId, ShippingAddressOrder shippingAddressOrder, string userInstructions, DateTime deliveryDate)
        {
            //To Do: add validation, error handling strategies, error notification strategies

            return new Order
            {
                UserProfileId = userProfileId,
                ProductList = productList,
                Price = price,
                ShipmentTypeId = shipmentTypeId,
                ShippingAddressOrder = shippingAddressOrder,
                UserInstructions = userInstructions,
                DeliveryDate = deliveryDate,
                DateCreated = DateTime.UtcNow
            };
        }

        public void UpdateOrder(Order order)
        {
            UserProfileId = order.UserProfileId;
            ProductList = order.ProductList;
            Price = order.Price;
            ShipmentTypeId = order.ShipmentTypeId;
            ShippingAddressOrder = order.ShippingAddressOrder;
            UserInstructions = order.UserInstructions;
            DeliveryDate = order.DeliveryDate;
            DateCreated = DateTime.UtcNow;
        }

        public void EmptyProductList()
        {
            ProductList = new List<Product>();
        }
    }
}
