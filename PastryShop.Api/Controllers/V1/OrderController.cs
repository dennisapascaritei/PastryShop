
using Microsoft.AspNetCore.Authorization;
using PastryShop.Application.Orders.Commands;

namespace PastryShop.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public OrderController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllOrdersQuery(), cancellationToken);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            var mapped = _mapper.Map<List<OrderResponse>>(result.Payload);

            return Ok(mapped);
        }

        [HttpGet]
        [Route(ApiRoutes.Order.OrderId)]
        [ValidateGuid("orderId")]
        public async Task<IActionResult> GetOrderById(string orderId, CancellationToken cancellationToken)
        {
            var orderGuid = Guid.Parse(orderId);
            var query = new GetOrderByIdQuery { OrderId = orderGuid };

            var result = await _mediator.Send(query, cancellationToken);

            if (result.IsError) return HandleErrorResponse(result.Errors);

            var mapped = _mapper.Map<OrderResponse>(result.Payload);

            return Ok(mapped);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateRequest newOrder, CancellationToken cancellationToken)
        {
            var userProfileId = HttpContext.GetUserProfileIdClaimValue();
            var command = new OrderCreateCommand
            {
                UserProfileId = userProfileId,
                ProductList = newOrder.ProductList,
                Price = newOrder.Price,
                DeliveryDate = newOrder.DeliveryDate,
                ShipmentTypeId = newOrder.ShipmentTypeId,
                County = newOrder.County,
                City = newOrder.City,
                Address = newOrder.Address,
                PostCode = newOrder.PostCode,
                UserInstructions = newOrder.UserInstructions
            };

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsError) return HandleErrorResponse(result.Errors);

            var mapped = _mapper.Map<OrderResponse>(result.Payload);

            return Ok(mapped);
        }

        [HttpPut]
        [Route(ApiRoutes.Order.OrderId)]
        [ValidateGuid("orderId")]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderUpdateRequest updatedOrder, string orderId, CancellationToken cancellationToken)
        {
            var userProfileId = HttpContext.GetUserProfileIdClaimValue();
            var command = new OrderUpdateCommand
            {
                OrderId = Guid.Parse(orderId),
                UserProfileId = userProfileId,
                ProductList = updatedOrder.ProductList,
                Price = updatedOrder.Price,
                DeliveryDate = updatedOrder.DeliveryDate,
                ShipmentTypeId = updatedOrder.ShipmentTypeId,
                County = updatedOrder.County,
                City = updatedOrder.City,
                Address = updatedOrder.Address,
                PostCode = updatedOrder.PostCode,
                UserInstructions = updatedOrder.UserInstructions
            };

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsError) return HandleErrorResponse(result.Errors);

            return NoContent();
        }

        [HttpDelete]
        [Route(ApiRoutes.Order.OrderId)]
        [ValidateGuid("orderId", "userProfileId")]
        public async Task<IActionResult> DeleteOrder(string orderId, CancellationToken cancellationToken)
        {
            var userProfileId = HttpContext.GetUserProfileIdClaimValue();
            var command = new OrderDeleteCommand 
            { 
                OrderId = Guid.Parse(orderId),
                UserProfileId = userProfileId
            };

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsError) return HandleErrorResponse(result.Errors);

            return NoContent();
        }
    }
}
