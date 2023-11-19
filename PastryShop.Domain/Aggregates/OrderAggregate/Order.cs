
using PastryShop.Domain.Aggregates.ShipmentTypeAggregate;
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
        public double Price { get; private set; }
        public List<LineItem> LineItems { get; private set; } = new();
        public ShipmentTypeOrder ShipmentType { get; private set; }
        public ShippingAddressOrder ShippingAddress { get; private set; }
        public string UserInstructions { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime DeliveryDate { get; private set;}

        public static Order CreateOrder(Guid userProfileId, double price, ShipmentTypeOrder shipmentType, ShippingAddressOrder shippingAddress, 
            string userInstructions, DateTime deliveryDate)
        {
            //To Do: add validation, error handling strategies, error notification strategies
            var order = new Order
            {
                OrderId = Guid.NewGuid(),
                UserProfileId = userProfileId,
                Price = price,
                ShipmentType = shipmentType,
                ShippingAddress = shippingAddress,
                UserInstructions = userInstructions,
                DeliveryDate = deliveryDate,
                DateCreated = DateTime.UtcNow
            };
            return order;
        }

        public void UpdateOrder(Order order)
        {
            UserProfileId = order.UserProfileId;
            //LineItems = order.LineItems;
            Price = order.Price;
            ShipmentType = order.ShipmentType;
            ShippingAddress = order.ShippingAddress;
            UserInstructions = order.UserInstructions;
            DeliveryDate = order.DeliveryDate;
            DateCreated = DateTime.UtcNow;
        }

        public void AddLineItem(Product product, Guid orderId)
        {
            var lineItem = new LineItem(orderId, product.ProductId, product.Name, product.Description, product.Price, product.Weight, product.ImageURL);
            LineItems.Add(lineItem);
        }
        public void RemoveLineItem(LineItem lineItem)
        {
            LineItems.Remove(lineItem);
        }
        public void AddShipmentTypeToOrder(ShipmentType shipmentType)
        {
            var type = new ShipmentTypeOrder(shipmentType.Name, shipmentType.Price);
            ShipmentType = type;
        }

        public void AddShippingAddressOrder(ShippingAddress shipmentAddress)
        {
            var address = new ShippingAddressOrder(shipmentAddress.County, shipmentAddress.City, shipmentAddress.Address, shipmentAddress.PostCode);
            ShippingAddress = address;
        }

        public void EmptyLineItemstList()
        {
            LineItems.Clear();
        }
    }
}
