using Auth.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;

namespace Auth.Application.Commands.SetUserBlackList
{
    public class SetUserBlackListCommandHandler : IRequestHandler<SetUserBlackListCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SetUserBlackListCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(SetUserBlackListCommand request, CancellationToken cancellationToken)
        {
            var targetUser = await _unitOfWork.Users.SingleOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken)
                ?? throw new Exception($"Пользователь с ID {request.UserId} не найден!");

            var blackUser = new BlacklistEntity
            {
                UserId = request.UserId,
                BlacklistedAt = DateTime.UtcNow
            };
            targetUser.ModifiedAt = DateTime.UtcNow;

            await _unitOfWork.Blacklist.AddAsync(blackUser, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
