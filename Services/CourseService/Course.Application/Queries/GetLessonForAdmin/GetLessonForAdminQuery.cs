using Course.Domain.Models;
using MediatR;

namespace Course.Application.Queries.GetLessonForAdmin
{
    public record GetLessonForAdminQuery : IRequest<LessonEntity>
    {
        public Guid LessonId { get; set; }
    }
}
