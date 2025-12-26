using MediatR;

namespace Course.Application.Commands.EditTest
{
    public record EditTestCommand : IRequest
    {
        public Guid TestId { get; set; }
        public Guid LessonId { get; set; }
        public string Name { get; set; }
        public int QuestionCount { get; set; }
        public int AttemptRestriction { get; set; }
        public int PassingScore { get; set; }
        public int TimeLimitMinutes { get; set; }
        public bool IsActive { get; set; }
    }
}
