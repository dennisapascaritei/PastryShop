namespace PastryShop.Api.Contracts.ShipmentTypes.Response
{
    public class ShipmentTypeResponse
    {
        public Guid ShipmentTypeId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
