using PastryShop.Application.Products.Queries;

namespace PastryShop.Api
{
    public class ApiRoutes
    {
        public const string BaseRoute = "api/v{version:apiVersion}/[controller]";
        public class Products
        {
            public const string ProductId = "{productId}";
        }

        public class UserProfiles
        {
            public const string UserProfileId = "{userProfileId}";
        }
        public class ShipmentTypes
        {
            public const string ShipmentTypeId = "{shipmentTypeId}";
        }
        public class Order
        {
            public const string OrderId = "{orderId}";
        }
        public class Identity
        {
            public const string Login = "login";
            public const string Registration = "registration";
            public const string IdentityById = "{identityUserId}";
        }
    }
}
