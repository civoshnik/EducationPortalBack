using MediatR;

namespace Course.Application.Commands.Lessons.CreateLesson
{
    public record CreateLessonCommand : IRequest
    {
        public Guid CourseId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string VideoURL { get; set; }
    }
}
