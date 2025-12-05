using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;

namespace Order.Application.Command.RemoveFromCart
{
    public class RemoveFromCartCommandHandler : IRequestHandler<RemoveFromCartCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveFromCartCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
        {
            var item = await _unitOfWork.CartItems
                .FirstOrDefaultAsync(i => i.UserId == request.UserId && i.ServiceId == request.ServiceId, cancellationToken);

            if (item == null) return;

            _unitOfWork.CartItems.Remove(item);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
