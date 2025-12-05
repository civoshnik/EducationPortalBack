using Course.Domain.Models;
using MediatR;
using Shared.Application.Models;

namespace Course.Application.Queries.GetPaginatedLessonList
{
    public record GetPaginatedLessonListQuery(int Page, int PageSize) : IRequest<PaginatedResult<LessonEntity>>;

}
