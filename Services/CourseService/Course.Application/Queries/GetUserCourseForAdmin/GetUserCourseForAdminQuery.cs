using Course.Domain.Models;
using MediatR;

namespace Course.Application.Queries.GetUserCourseForAdmin
{
    public record GetUserCourseForAdminQuery : IRequest<List<CourseEntity>>
    {
        public Guid UserId { get; set; }
    }
}
