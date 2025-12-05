using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Order.Application.Command.AddToCart;
using Order.Application.Command.RemoveFromCart;
using Order.Application.Queries;
using Shared.Application.Interfaces;

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

        [HttpGet("getPaginated")]
        public async Task<ActionResult<object>> GetPaginated([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var items = await _mediator.Send(new GetPaginatedServicesQuery(page, pageSize));
            var totalCount = await _unitOfWork.Services.CountAsync();
            return Ok(new { items, totalCount });
        }

        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartCommand command)
        {
            var cartId = await _mediator.Send(command);

            return Ok(new { cartId });
        }

        //[HttpGet("getCart")]
        //public async Task<ActionResult<CartEntity>> GetCart([FromQuery] GetUserCartQuery query)
        //{
        //    var cart = await _mediator.Send(query);
        //    return Ok(cart);
        //}

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
    }
}
