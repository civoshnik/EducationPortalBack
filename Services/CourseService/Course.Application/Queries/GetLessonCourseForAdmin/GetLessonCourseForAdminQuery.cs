using Course.Domain.Models;
using MediatR;

namespace Course.Application.Queries.GetLessonCourseForAdmin
{
    public record GetLessonCourseForAdminQuery : IRequest<List<LessonEntity>>
    {
        public Guid CourseId { get; set; }
    }
}
