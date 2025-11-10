using Course.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Course.Application.Queries.GetCourseList;
using Course.Application.Commands.CreateCourse;
namespace Course.API.Controllers
{
    public class CourseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CourseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("paged")]
        public async Task<ActionResult<List<CourseEntity>>> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(new GetCourseListQuery(page, pageSize));
            return Ok(result);
        }

        [HttpPost("createCourse")]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
