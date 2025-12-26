using Course.Domain.Models;
using MediatR;

namespace Course.Application.Queries.GetTestsByCourse
{
    public record GetTestsByCourseQuery(Guid CourseId) : IRequest<List<TestEntity>>;
}
