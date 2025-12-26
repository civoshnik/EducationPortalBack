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
using Course.Application.Queries.GetPaginatedTestList;
using Course.Application.Queries.GetTestById;
using Course.Application.Commands.CreateTest;
using Course.Application.Commands.CreateQuestion;
using Course.Application.Commands.EditTest;
using Course.Application.Commands.DeleteTest;
using Course.Application.Queries.GetTestList;
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

        [HttpGet("paginatedTestList")]
        public async Task<ActionResult<PaginatedResult<CourseEntity>>> GetPaginatedTestList([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(new GetPaginatedTestListQuery(page, pageSize));
            return Ok(result);
        }

        //[HttpGet("testDetail/{testId}")]
        //public async Task<ActionResult<TestEntity>> GetTestById(Guid testId, CancellationToken cancellationToken)
        //{
        //    var query = new GetTestByIdQuery { TestId = testId };
        //    var test = await _mediator.Send(query, cancellationToken);
        //    return Ok(test);
        //}

        [HttpPost("createTest")] 
        public async Task<IActionResult> CreateTest([FromBody] CreateTestCommand command) 
        {
            await _mediator.Send(command); 
            return Ok(); 
        }

        [HttpPost("editTest")]
        public async Task<IActionResult> EditTest([FromBody] EditTestCommand command) 
        { 
            await _mediator.Send(command);
            return Ok(); 
        }

        [HttpDelete("deleteTest/{id}")] 
        public async Task<IActionResult> DeleteTest(Guid id) 
        {
            await _mediator.Send(new DeleteTestCommand { TestId = id });
            return Ok(); 
        }

        [HttpGet("getTestsList")]
        public async Task<ActionResult<List<TestEntity>>> GetTestsList()
        {
            var result = await _mediator.Send(new GetTestListQuery());
            return Ok(result);
        }
    }
}

