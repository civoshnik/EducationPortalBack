using MediatR;

namespace Course.Application.Commands.Lessons.UpdateLesson
{
    public record UpdateLessonCommand : IRequest
    {
        public Guid LessonId { get; set; }
        public Guid CourseId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string VideoURL { get; set; }
    }
}
