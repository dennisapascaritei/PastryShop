namespace PastryShop.Api.Contracts.UserProfiles.Response
{
    public class BasicInfoResponse
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string EmailAddress { get; private set; }
        public string Phone { get; private set; }
        public ShippingAddressResponse ShippingAddress { get; private set; }
    }
}
