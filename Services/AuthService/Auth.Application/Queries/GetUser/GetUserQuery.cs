using Auth.Domain.Models;
using MediatR;

namespace Auth.Application.Queries.GetUser
{
    public record GetUserQuery : IRequest<UserEntity>
    {
        public required Guid Id { get; set; }
    }
}
