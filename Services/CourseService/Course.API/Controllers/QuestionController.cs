using Course.Application.Commands.CreateQuestion;
using Course.Application.Commands.DeleteQuestion;
using Course.Application.Commands.DeleteTest;
using Course.Application.Commands.EditQuestion;
using Course.Application.Queries.GetCourseForAdmin;
using Course.Application.Queries.GetCourseList;
using Course.Application.Queries.GetPaginatedQuestionList;
using Course.Application.Queries.GetQuestionById;
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

        [HttpGet("{questionId}")]
        public async Task<ActionResult<QuestionEntity>> GetQuestionById(Guid questionId, CancellationToken cancellationToken)
        {
            var query = new GetQuestionByIdQuery { QuestionId = questionId };
            var question = await _mediator.Send(query, cancellationToken);
            return Ok(question);
        }

        [HttpPost("createQuestion")]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("editQuestion")] 
        public async Task<IActionResult> EditQuestion([FromBody] EditQuestionCommand command)
        { 
            await _mediator.Send(command); 
            return Ok(); 
        }

        [HttpDelete("deleteQuestion/{id}")]
        public async Task<IActionResult> DeleteQuestion(Guid id)
        {
            await _mediator.Send(new DeleteQuestionCommand { QuestionId = id });
            return Ok();
        }

    }
}
