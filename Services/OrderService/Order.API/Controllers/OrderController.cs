using Course.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Command.CreateOrder;
using Order.Application.Queries.GetOrdersByService;
using Order.Application.Queries.GetPaginatedOrder;
using Order.Application.Queries.GetUserOrders;
using Order.Domain.Models;
using Shared.Application.Models;

namespace Order.API.Controllers
{
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateOrderCommand command)
        {
            var orderId = await _mediator.Send(command);
            return Ok(new
            {
                orderId,
                totalPrice = command.Services.Sum(s => s.Price * s.Quantity),
                status = OrderStatus.Confirmed.ToString(),
                createdAt = DateTime.UtcNow
            });
        }

        [HttpGet("user")]
        public async Task<ActionResult<List<OrderEntity>>> GetUserOrders([FromQuery] GetUserOrdersQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("paginatedList")]
        public async Task<ActionResult<PaginatedResult<OrderEntity>>> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(new GetPaginatedOrderQuery(page, pageSize));
            return Ok(result);
        }

        [HttpGet("getOrdersByService")]
        public async Task<ActionResult<List<OrderEntity>>> GetOrdersByService([FromQuery] Guid serviceId)
        {
            var result = await _mediator.Send(new GetOrdersByServiceQuery { ServiceId = serviceId });
            return Ok(result);
        }


    }
}
