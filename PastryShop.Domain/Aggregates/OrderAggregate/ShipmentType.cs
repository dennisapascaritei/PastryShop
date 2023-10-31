namespace PastryShop.Domain.Aggregates.OrderAggregate
{
    public class ShipmentType
    {
        private ShipmentType()
        {

        }
        public Guid ShipmentTypeId { get; private set; }
        public string Name { get; private set; }
        public double Price { get; private set; }
        public DateTime LastUpdated { get; private set; }

        public static ShipmentType CreateShipmentType(string name, double price)
        {
            //To Do: add validation, error handling strategies, error notification strategies

            return new ShipmentType()
            {
                Name = name,
                Price = price,
                LastUpdated = DateTime.UtcNow
            };
        }

        public void UpdateShipmentType(ShipmentType shipmentType)
        {
            Name = shipmentType.Name;
            Price = shipmentType.Price;
            LastUpdated = DateTime.UtcNow;
        }
    }
}