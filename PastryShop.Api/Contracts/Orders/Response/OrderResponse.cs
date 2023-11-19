using PastryShop.Api.Contracts.LineItems.Response;
using PastryShop.Domain.Aggregates.ShipmentTypeAggregate;

namespace PastryShop.Api.Contracts.Orders.Response
{
    public class OrderResponse
    {
        public Guid OrderId { get; set; }
        public Guid UserProfileId { get; set; }
        public List<LineItemResponse> LineItems { get; set; }
        public double Price { get; set; }
        public ShipmentTypeOrderResponse ShipmentType { get; set; }
        public ShippingAddressOrder ShippingAddressOrder { get; set; }
        public string UserInstructions { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}
