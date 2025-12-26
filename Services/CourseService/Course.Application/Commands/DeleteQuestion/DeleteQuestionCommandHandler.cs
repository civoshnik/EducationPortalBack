using Course.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;

namespace Course.Application.Commands.DeleteQuestion
{
    public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteQuestionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            var targetQuestion = await _unitOfWork.Questions
                .FirstOrDefaultAsync(q => q.QuestionId == request.QuestionId, cancellationToken)
                ?? throw new Exception($"Вопрос с ID {request.QuestionId} не найден!");

            _unitOfWork.Questions.Remove(targetQuestion);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
