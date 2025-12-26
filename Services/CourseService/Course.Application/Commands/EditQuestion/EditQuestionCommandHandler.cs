using Course.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;

namespace Course.Application.Commands.EditQuestion
{
    public class EditQuestionCommandHandler : IRequestHandler<EditQuestionCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditQuestionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(EditQuestionCommand request, CancellationToken cancellationToken)
        {
            var targetQuestion = await _unitOfWork.Questions.FirstOrDefaultAsync(q => q.QuestionId == request.QuestionId, cancellationToken)
                ?? throw new Exception($"Вопрос с ID {request.QuestionId} не найден!");

            targetQuestion.TestId = request.TestId;
            targetQuestion.Text = request.Text;
            targetQuestion.Type = request.Type;
            targetQuestion.ModifiedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
