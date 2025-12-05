using Course.Domain.Models;
using MediatR;

namespace Course.Application.Queries.GetCourseForAdmin
{
    public record GetCourseForAdminQuery : IRequest<CourseEntity>
    {
        public Guid CourseId { get; set; }
    }
}
