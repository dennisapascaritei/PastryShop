namespace PastryShop.Domain.Aggregates.OrderAggregate
{
    public class ShippingAddressOrder
    {
        internal ShippingAddressOrder(string county, string city, string address, string postCode)
        {
            County = county;
            City = city;
            Address = address;
            PostCode = postCode;
        }

        public string County { get; private set; }
        public string City { get; private set; }
        public string Address { get; private set; }
        public string PostCode { get; private set; }

        public static ShippingAddressOrder CreateShippingAddressOrder(string county, string city, string address, string postCode)
        {
            //To Do: add validation, error handling strategies, error notification strategies

            return new ShippingAddressOrder(county, city, address, postCode)
            {
                County = county,
                City = city,
                Address = address,
                PostCode = postCode
            };
        }

    }
}