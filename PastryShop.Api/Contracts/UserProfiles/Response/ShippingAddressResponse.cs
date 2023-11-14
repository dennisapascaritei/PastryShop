namespace PastryShop.Api.Contracts.UserProfiles.Response
{
    public class ShippingAddressResponse
    {
        public Guid ShippingAddressId { get; private set; }
        public string County { get; private set; }
        public string City { get; private set; }
        public string Address { get; private set; }
        public string PostCode { get; private set; }
    }
}