using PastryShop.Domain.Aggregates.ShipmentTypeAggregate;

namespace PastryShop.Domain.Aggregates.OrderAggregate
{
    public class ShipmentTypeOrder
    {
        internal ShipmentTypeOrder(string name, double price)
        {
            Name = name;
            Price = price;
            LastUpdated = DateTime.UtcNow;
        }
        public string Name { get; private set; }
        public double Price { get; private set; }
        public DateTime LastUpdated { get; private set; }

        public static ShipmentTypeOrder CreateShipmentTypeOrder(string name, double price)
        {
            //To Do: add validation, error handling strategies, error notification strategies

            return new ShipmentTypeOrder(name, price)
            {
                Name = name,
                Price = price,
                LastUpdated = DateTime.UtcNow
            };
        }

        public void UpdateShipmentTypeOrder(ShipmentTypeOrder shipmentType)
        {
            Name = shipmentType.Name;
            Price = shipmentType.Price;
            LastUpdated = DateTime.UtcNow;
        }
    }
}