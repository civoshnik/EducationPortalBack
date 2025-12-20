using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;

namespace Auth.Application.Commands.CancelUSerBlackList
{
    public class CancelUserBlackListQueryHandler : IRequestHandler<CancelUserBlackListQuery>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CancelUserBlackListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CancelUserBlackListQuery request, CancellationToken cancellationToken)
        {
            var targetUser = await _unitOfWork.Blacklist.SingleOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken)
                ?? throw new Exception($"Пользователь с ID {request.UserId} не найден!");

            _unitOfWork.Blacklist.Remove(targetUser);

            var user = await _unitOfWork.Users.SingleOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken)
                ?? throw new Exception($"Пользователь с ID {request.UserId} не найден!");

            user.ModifiedAt = DateTime.UtcNow;
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
