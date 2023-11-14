using PastryShop.Domain.Aggregates.ShipmentTypeAggregate;

namespace PastryShop.Api.Contracts.Orders.Response
{
    public class OrderResponse
    {
        public Guid OrderId { get; set; }
        public Guid UserProfileId { get; set; }
        public List<ProductResponse> ProductList { get; set; }
        public double Price { get; set; }
        public ShipmentTypeResponse ShipmentType { get; set; }
        public ShippingAddressOrder ShippingAddressOrder { get; set; }
        public string UserInstructions { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}
