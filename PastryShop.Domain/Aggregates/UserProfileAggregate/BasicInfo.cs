
namespace PastryShop.Domain.Aggregates.UserProfileAggregate
{
    public class BasicInfo
    {
        private BasicInfo()
        {

        }
        public string FirstName { get; private set; } 
        public string LastName { get; private set;}
        public string EmailAddress { get; private set;}
        public string Phone { get; private set;}
        public ShippingAddress ShippingAddress { get; private set;}

        public static BasicInfo CreateBasicInfo(string firstName, string lastName, string emailAddress, string phone, ShippingAddress shippingAddress)
        {
            //To Do: add validation, error handling strategies, error notification strategies

            return new BasicInfo
            {
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = emailAddress,
                Phone = phone,
                ShippingAddress = shippingAddress
            };
        }
    }
}
