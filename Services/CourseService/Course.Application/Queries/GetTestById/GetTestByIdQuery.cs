using Course.Domain.Models;
using MediatR;

namespace Course.Application.Queries.GetTestById
{
    public record GetTestByIdQuery(Guid TestId) : IRequest<TestEntity?>;
}
