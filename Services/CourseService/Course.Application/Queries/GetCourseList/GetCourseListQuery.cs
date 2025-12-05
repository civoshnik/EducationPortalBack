using Course.Domain.Models;
using MediatR;
using Shared.Application.Models;

namespace Course.Application.Queries.GetCourseList
{
    public record GetCourseListQuery(int Page, int PageSize) : IRequest<PaginatedResult<CourseEntity>>;
}
