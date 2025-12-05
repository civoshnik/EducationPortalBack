using MediatR;

namespace Course.Application.Commands.Lessons.DeleteLesson
{
    public record DeleteLessonCommand : IRequest
    {
        public Guid LessonId { get; set; }
    }
}
