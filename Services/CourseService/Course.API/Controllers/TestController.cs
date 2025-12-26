using Course.Application.Commands.SubmitTest;
using Course.Application.Queries.GetQuestionsByTest;
using Course.Application.Queries.GetQuestionsWithAnswersByTest;
using Course.Application.Queries.GetTestById;
using Course.Application.Queries.GetTestsByCourse;
using Course.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Course.API.Controllers
{
    public class TestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("testDetail/{testId}")]
        public async Task<ActionResult<TestEntity>> GetTestById(Guid testId, CancellationToken cancellationToken)
        {
            var query = new GetTestByIdQuery(testId);
            var test = await _mediator.Send(query, cancellationToken);
            if (test == null) return NotFound();
            return Ok(test);
        }

        [HttpGet("byCourse/{courseId}")]
        public async Task<ActionResult<List<TestEntity>>> GetTestsByCourse(Guid courseId, CancellationToken cancellationToken)
        {
            var query = new GetTestsByCourseQuery(courseId);
            var tests = await _mediator.Send(query, cancellationToken);
            return Ok(tests);
        }

        [HttpGet("{testId}/questions")]
        public async Task<ActionResult<List<QuestionEntity>>> GetQuestions(Guid testId, CancellationToken cancellationToken)
        {
            var query = new GetQuestionsByTestQuery(testId);
            var questions = await _mediator.Send(query, cancellationToken);

            if (questions == null || !questions.Any())
                return NotFound($"Для теста {testId} вопросы не найдены");

            return Ok(questions);
        }


        [HttpPost("{testId}/submit")]
        public async Task<ActionResult<SubmitResult>> Submit(Guid testId, [FromBody] SubmitDto dto, CancellationToken cancellationToken)
        {
            var command = new SubmitTestCommand(testId, dto.UserId, dto.Answers, dto.TimeSpentSeconds);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{testId}/questions-with-answers")]
        public async Task<ActionResult<List<QuestionWithAnswersDto>>> GetQuestionsWithAnswers(Guid testId, CancellationToken cancellationToken)
        {
            var query = new GetQuestionsWithAnswersByTestQuery(testId);
            var result = await _mediator.Send(query, cancellationToken);

            if (result == null || !result.Any())
                return NotFound($"Для теста {testId} вопросы не найдены");

            return Ok(result);
        }

    }

    public class SubmitDto
    {
        public Guid UserId { get; set; }
        public List<AnswerDto> Answers { get; set; } = new();
        public int TimeSpentSeconds { get; set; }
    }
}
