using MediatR;

namespace Course.Application.Commands.EditCourse
{
    public record EditCourseCommand : IRequest
    {
        public Guid CourseId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int DurationHours { get; set; }
        public string Level { get; set; }
        public string Creator { get; set; }
        public bool IsPublished { get; set; }
    }
}
