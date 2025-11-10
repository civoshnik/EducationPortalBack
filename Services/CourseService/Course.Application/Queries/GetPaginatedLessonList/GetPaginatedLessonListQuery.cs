using Course.Domain.Models;
using MediatR;

namespace Course.Application.Queries.GetPaginatedLessonList
{
    public record GetPaginatedLessonListQuery(int Page, int PageSize) : IRequest<List<LessonEntity>>
    {
    }
}
