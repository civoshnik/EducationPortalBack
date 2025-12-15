using Auth.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;

namespace Auth.Application.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConfirmEmailCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var tokenEntity = await _unitOfWork.EmailConfirmTokens
                .SingleOrDefaultAsync(t => t.UserId == request.UserId && t.Token == request.Token, cancellationToken);

            if (tokenEntity == null || tokenEntity.IsUsed || tokenEntity.ExpiresAt < DateTime.UtcNow)
                return false;

            tokenEntity.IsUsed = true;

            var user = await _unitOfWork.Users
                .SingleOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);

            if (user == null)
                return false;

            user.EmailConfirmed = true;
            user.ModifiedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
