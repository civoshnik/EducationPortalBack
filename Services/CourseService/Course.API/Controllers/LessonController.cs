using Course.Application.Commands.Lessons.CreateLesson;
using Course.Application.Commands.Lessons.DeleteLesson;
using Course.Application.Commands.Lessons.UpdateLesson;
using Course.Application.Queries.GetLessonForAdmin;
using Course.Application.Queries.GetPaginatedLessonList;
using Course.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Models;

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
        public async Task<ActionResult<PaginatedResult<LessonEntity>>> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(new GetPaginatedLessonListQuery(page, pageSize));
            return Ok(result);
        }

        [HttpGet("{lessonId}/details")]
        public async Task<ActionResult<LessonEntity>> GetLessonById(Guid lessonId)
        {
            var lesson = await _mediator.Send(new GetLessonForAdminQuery { LessonId = lessonId });
            return Ok(lesson);
        }

        [HttpDelete("{lessonId}/delete")]
        public async Task<IActionResult> DeleteLesson(Guid lessonId)
        {
            await _mediator.Send(new DeleteLessonCommand { LessonId = lessonId });
            return Ok();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateLesson([FromBody] CreateLessonCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("edit")]
        public async Task<IActionResult> UpdateLesson([FromBody] UpdateLessonCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
