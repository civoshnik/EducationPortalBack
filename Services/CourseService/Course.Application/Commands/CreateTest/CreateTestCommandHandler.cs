using Course.Domain.Models;
using MediatR;
using Shared.Application.Interfaces;

namespace Course.Application.Commands.CreateTest
{
    public class CreateTestCommandHandler : IRequestHandler<CreateTestCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTestCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CreateTestCommand request, CancellationToken cancellationToken)
        {
            var test = new TestEntity
            {
                TestId = Guid.NewGuid(),
                LessonId = request.LessonId,
                Name = request.Name,
                QuestionCount = request.QuestionCount,
                AttemptRestriction = request.AttemptRestriction,
                PassingScore = request.PassingScore,
                TimeLimitMinutes = request.TimeLimitMinutes,
                IsActive = request.IsActive,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Tests.AddAsync(test, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
