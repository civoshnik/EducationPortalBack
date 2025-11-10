using Auth.Application.Commands.Autorization;
using Auth.Application.Commands.RegisterUser;
using Auth.Application.Queries;
using Auth.Application.Queries.GetPaginatedStudentList;
using Auth.Application.Queries.GetPaginatedTeacherList;
using Auth.Application.Queries.GetUser;
using Auth.Domain.Models;
using Course.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Auth.API.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getUser/{id}")]
        public async Task<ActionResult<UserEntity?>> GetUser(Guid id)
        {
            var query = new GetUserQuery { Id = id };
            var user = await _mediator.Send(query);
            return Ok(user);
        }

        [HttpPost("registrationUser")]
        public async Task<IActionResult> RegistrationUser([FromBody] RegisterUserCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPost("autorizationUser")]
        public async Task<IActionResult> Autorization([FromBody] AutorizationCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("paginatedStudentList")]
        public async Task<ActionResult<List<UserEntity>>> GetPageGetPaginatedSrudentList([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(new GetPaginatedStudentListQuery(page, pageSize));
            return Ok(result);
        }

        [HttpGet("paginatedTeacherList")]
        public async Task<ActionResult<List<UserEntity>>> GetPaginatedTeacherList([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(new GetPaginatedTeacherListQuery(page, pageSize));
            return Ok(result);
        }
    }
}
