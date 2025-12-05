using Course.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Course.Application.Queries.GetCourseList;
using Course.Application.Commands.CreateCourse;
using Course.Application.Queries.GetCoursesByLevel;
using Course.Application.Queries.GetMyCourse;
using Shared.Application.Models;
using Course.Application.Queries.GetCourseForAdmin;
using Course.Application.Queries.GetLessonCourseForAdmin;
using Course.Application.Commands.DeleteCourseCommand;
using Course.Application.Queries.GetUserCourseForAdmin;
using Course.Application.Commands.EditCourse;
using Course.Application.Commands.GetCourseList;
using Course.Application.Commands.EnrollCourse;
using Course.Application.Commands.UpdateProgress;
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
        public async Task<ActionResult<PaginatedResult<CourseEntity>>> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
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

        [HttpGet("by-level/{level}")]
        public async Task<IActionResult> GetByLevel(string level)
        {
            return Ok(await _mediator.Send(new GetCoursesByLevelQuery(level)));
        }

        [HttpPost("enroll")]
        public async Task<IActionResult> Enroll(Guid userId, Guid courseId)
        {
            return Ok(await _mediator.Send(new EnrollCourseCommand(userId, courseId)));
        }

        [HttpGet("my/{userId}")]
        public async Task<IActionResult> GetMyCourse(Guid userId)
        {
            return Ok(await _mediator.Send(new GetMyCourseQuery(userId)));
        }

        [HttpPost("progress")]
        public async Task<IActionResult> UpdateProgress(UpdateProgressCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet("detail/{id}")]
        public async Task<ActionResult<CourseEntity>> GetDetailCourse(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetCourseForAdminQuery { CourseId = id };
            var course = await _mediator.Send(query, cancellationToken);
            return Ok(course);
        }


        [HttpGet("{courseId}/lessons")]
        public async Task<ActionResult<List<LessonEntity>>> GetCourseLessons(Guid courseId, CancellationToken cancellationToken)
        {
            var query = new GetLessonCourseForAdminQuery { CourseId = courseId };
            var lessons = await _mediator.Send(query, cancellationToken);
            return Ok(lessons);
        }

        [HttpDelete("{courseId}")]
        public async Task<IActionResult> DeleteCorse(Guid courseId, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteCourseForAdminCommand { CourseId = courseId }, cancellationToken);
            return Ok();
        }

        [HttpGet("{userId}/courses")]
        public async Task<ActionResult<List<CourseEntity>>> GetUserCourses(Guid userId)
        {
            var query = await _mediator.Send(new GetUserCourseForAdminQuery { UserId = userId });
            return Ok(query);
        }

        [HttpPost("editCourse")]
        public async Task<IActionResult> EditCourse([FromBody] EditCourseCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<CourseEntity>>> GetCourses()
        {
            var result = await _mediator.Send(new GetCourseListSelectQuery());
            return Ok(result);
        }
    }
}

