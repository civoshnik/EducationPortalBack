using Course.Application.Queries.GetCourseList;
using Course.Application.Queries.GetPaginatedQuestionList;
using Course.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Course.API.Controllers
{
    public class QuestionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuestionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("paginatedQueryList")]
        public async Task<ActionResult<List<QuestionEntity>>> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(new GetPaginatedQuestionListQuery(page, pageSize));
            return Ok(result);
        }
    }
}
