using Course.Application.Queries.GetPaginatedLessonList;
using Course.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Course.API.Controllers
{
    public class LessonController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LessonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("paginatedLessonList")]
        public async Task<ActionResult<List<LessonEntity>>> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(new GetPaginatedLessonListQuery(page, pageSize));
            return Ok(result);
        }
    }
}
