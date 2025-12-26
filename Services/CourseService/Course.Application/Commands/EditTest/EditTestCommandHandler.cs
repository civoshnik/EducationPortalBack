using Course.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;

namespace Course.Application.Commands.EditTest
{
    public class EditTestCommandHandler : IRequestHandler<EditTestCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditTestCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(EditTestCommand request, CancellationToken cancellationToken)
        {
            var targetTest = await _unitOfWork.Tests.SingleOrDefaultAsync(t => t.TestId == request.TestId, cancellationToken)
                ?? throw new Exception($"Тест с ID {request.TestId} не найден!");

            targetTest.LessonId = request.LessonId;
            targetTest.Name = request.Name;
            targetTest.QuestionCount = request.QuestionCount;
            targetTest.AttemptRestriction = request.AttemptRestriction;
            targetTest.PassingScore = request.PassingScore;
            targetTest.TimeLimitMinutes = request.TimeLimitMinutes;
            targetTest.IsActive = request.IsActive;
            targetTest.ModifiedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
