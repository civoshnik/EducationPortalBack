using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;

namespace Order.Application.Command.DeleteService
{
    public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteServiceCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new Exception(nameof(unitOfWork));
        }

        public async Task Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
        {
            var targetService = await _unitOfWork.Services.SingleOrDefaultAsync(s => s.ServiceId == request.ServiceId, cancellationToken)
                ?? throw new Exception($"Услуга с ID {request.ServiceId} не найдена!");

            _unitOfWork.Services.Remove(targetService);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
