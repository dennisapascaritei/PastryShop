namespace PastryShop.Domain.Aggregates.UserProfileAggregate
{
    public class ShippingAddress
    {
        private ShippingAddress() { }

        public Guid ShippingAddressId { get; private set; }
        public string County { get; private set; }
        public string City { get; private set; }
        public string Address { get; private set; }
        public string PostCode { get; private set; }

        public static ShippingAddress CreateShippingAddress(string country, string city, string address, string postCode)
        {
            //To Do: add validation, error handling strategies, error notification strategies

            return new ShippingAddress()
            {
                County = country,
                City = city,
                Address = address,
                PostCode = postCode
            };
        }

    }
}