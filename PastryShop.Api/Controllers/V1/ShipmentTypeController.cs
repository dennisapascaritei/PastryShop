
namespace PastryShop.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    public class ShipmentTypeController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ShipmentTypeController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllShipmentTypes(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllShipmentTypesQuery(), cancellationToken);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            var mapped = _mapper.Map<List<ShipmentTypeResponse>>(result.Payload);

            return Ok(mapped);
        }

        [HttpGet]
        [Route(ApiRoutes.ShipmentTypes.ShipmentTypeId)]
        [ValidateGuid("shipmentTypeId")]
        public async Task<IActionResult> GetShipmentTypeById(string shipmentTypeId, CancellationToken cancellationToken)
        {
            var shipmentTypeGuid = Guid.Parse(shipmentTypeId);
            var query = new GetShipmentTypeByIdQuery { ShipmentTypeId = shipmentTypeGuid };

            var result = await _mediator.Send(query, cancellationToken);

            if (result.IsError) return HandleErrorResponse(result.Errors);

            var mapped = _mapper.Map<ShipmentTypeResponse>(result.Payload);

            return Ok(mapped);
        }

        [HttpPost]
        public async Task<IActionResult> CreateShipmentType([FromBody] ShipmentTypeCreateRequest newShipmentType, CancellationToken cancellationToken)
        {
            var command = new CreateShipmentTypeCommand
            {
                Name = newShipmentType.Name,
                Price = newShipmentType.Price
            };

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsError) return HandleErrorResponse(result.Errors);

            var mapped = _mapper.Map<ShipmentTypeResponse>(result.Payload);

            return Ok(mapped);
        }

        [HttpPut]
        [Route(ApiRoutes.ShipmentTypes.ShipmentTypeId)]
        [ValidateGuid("shipmentTypeId")]
        public async Task<IActionResult> UpdateShipmentType([FromBody] ShipmentTypeUpdateRequest updatedShipmentType, string shipmentTypeId, CancellationToken cancellationToken)
        {
            var command = new UpdateShipmentTypeCommand
            {
                ShipmentTypeId = Guid.Parse(shipmentTypeId),
                Name = updatedShipmentType.Name,
                Price = updatedShipmentType.Price
            };

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsError) return HandleErrorResponse(result.Errors);

            return NoContent();
        }

        [HttpDelete]
        [Route(ApiRoutes.ShipmentTypes.ShipmentTypeId)]
        [ValidateGuid("shipmentTypeId")]
        public async Task<IActionResult> DeleteShipmentType(string shipmentTypeId, CancellationToken cancellationToken)
        {
            var command = new DeleteShipmentTypeCommand { ShipmentTypeId = Guid.Parse(shipmentTypeId) };

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsError) return HandleErrorResponse(result.Errors);

            return NoContent();
        }

    }
}
