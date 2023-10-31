
namespace PastryShop.Application.Orders.Commands
{
    public class OrderUpdateCommand : IRequest<OperationResult<Order>>
    {
        public Guid OrderId { get; set; }
        public Guid UserProfileId { get; set; }
        public List<Guid> ProductList { get; set; }
        public double Price { get; set; }
        public DateTime DeliveryDate { get; set; }
        public Guid ShipmentTypeId { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string UserInstructions { get; set; }
    }
}
