
namespace PastryShop.Application.Enums
{
    public enum ErrorCode
    {
        NotFound = 404,
        ServerError = 500,
        IdentityCreationFailed = 202,
        OrderUpdateNotPossible = 300,
        OrderDeleteNotPossible = 301,
        IdentityUserAlreadyExists = 320,
        IdentityUserDoesNotExist = 321,
        IncorrectPassword = 322,
        UnauthorizedAccountRemoval = 323,

        UnknownError = 999
    }
}
