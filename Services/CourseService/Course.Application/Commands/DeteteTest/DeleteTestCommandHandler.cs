using Course.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;

namespace Course.Application.Commands.DeleteTest
{
    public class DeleteTestCommandHandler : IRequestHandler<DeleteTestCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTestCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteTestCommand request, CancellationToken cancellationToken)
        {
            var targetTest = await _unitOfWork.Tests.FirstOrDefaultAsync(t => t.TestId == request.TestId, cancellationToken)
                ?? throw new Exception($"Тест с ID {request.TestId} не найден!");

            _unitOfWork.Tests.Remove(targetTest);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
