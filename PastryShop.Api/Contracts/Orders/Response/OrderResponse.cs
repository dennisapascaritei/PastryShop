using PastryShop.Domain.Aggregates.OrderAggregate;

namespace PastryShop.Api.Contracts.Orders.Response
{
    public class OrderResponse
    {
        public Guid OrderId { get; private set; }
        public Guid UserProfileId { get; private set; }
        public List<ProductResponse> ProductList { get; private set; }
        public double Price { get; private set; }
        public ShipmentType ShipmentType { get; private set; }
        public ShippingAddressOrder ShippingAddressOrder { get; private set; }
        public string UserInstructions { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime DeliveryDate { get; private set; }
    }
}
