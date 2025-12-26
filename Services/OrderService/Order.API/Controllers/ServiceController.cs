using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Order.Application.Command.AddToCart;
using Order.Application.Command.CreateService;
using Order.Application.Command.DeleteService;
using Order.Application.Command.EditService;
using Order.Application.Command.RemoveFromCart;
using Order.Application.Queries;
using Order.Application.Queries.GetPaginatedOrder;
using Order.Application.Queries.GetServiceById;
using Order.Application.Queries.GetUserCart;
using Order.Domain.Models;
using Shared.Application.Interfaces;
using Shared.Application.Models;

namespace Order.API.Controllers
{
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public ServicesController(IMediator mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("paged")]
        public async Task<ActionResult<PaginatedResult<OrderEntity>>> GetPagedOrders([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = await _mediator.Send(new GetPaginatedOrderQuery(page, pageSize));
            return Ok(result);
        }


        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartCommand command)
        {
            var cartId = await _mediator.Send(command);

            return Ok(new { cartId });
        }

        [HttpGet("getCart")]
        public async Task<ActionResult<List<CartItemDto>>> GetCart([FromQuery] Guid userId)
        {
            var items = await _mediator.Send(new GetUserCartQuery { UserId = userId });
            return Ok(items);
        }




        [HttpDelete("removeFromCart")]
        public async Task<IActionResult> RemoveFromCart([FromQuery] Guid userId, [FromQuery] Guid serviceId)
        {
            await _mediator.Send(new RemoveFromCartCommand
            {
                UserId = userId,
                ServiceId = serviceId
            });
            return NoContent();
        }

        [HttpGet("getServiceById/{serviceId}")]
        public async Task<ActionResult<ServiceEntity>> GetServiceById(Guid serviceId)
        {
            var query = new GetServiceByIdQuery { ServiceId = serviceId };
            var service = await _mediator.Send(query);
            return Ok(service);
        }

        [HttpPost("createService")]
        public async Task<IActionResult> CreateService([FromBody] CreateServiceCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet("getPaginatedServiceList")]
        public async Task<ActionResult<PaginatedResult<ServiceEntity>>> GetPaginatedServiceList([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = await _mediator.Send(new GetPaginatedServicesQuery(page, pageSize));
            return Ok(result);
        }

        [HttpDelete("{serviceId}")]
        public async Task<IActionResult> DeleteService(Guid serviceId)
        {
            await _mediator.Send(new DeleteServiceCommand { ServiceId = serviceId });
            return Ok();
        }

        [HttpPost("editService")]
        public async Task<IActionResult> EditService([FromBody] EditServiceCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }


    }
}
