
namespace PastryShop.Application.Orders
{
    public class OrderErrorMessages
    {
        public const string OrderListIsEmpty = "Currently there is no order in the database";
        public const string OrderNotFound = "No order found with id {0}";
        public const string OrderHasNoValidProductsAdded = "There is no valid product added to the order.";
        public const string OrderDeleteNotPossible = "Only the owner of an order can delete it.";
        public const string OrderUpdateNotPossible = "Order update not possible because it's not the post owner that initiates the update";
    }
}
