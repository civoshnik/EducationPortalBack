using MediatR;

namespace Course.Application.Commands.DeleteCourseCommand
{
    public record DeleteCourseForAdminCommand : IRequest
    {
        public Guid CourseId { get; set; }
    }
}
