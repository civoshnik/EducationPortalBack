using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var user = await _unitOfWork.Users
                .SingleOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);
            if (user is null) return false;

            var record = await _unitOfWork.EmailConfirmTokens
                .SingleOrDefaultAsync(x =>
                    x.UserId == request.UserId &&
                    x.Token == request.Token &&
                    !x.IsUsed &&
                    x.ExpiresAt > DateTime.UtcNow,
                    cancellationToken);

            if (record is null) return false;

            record.IsUsed = true;
            user.IsConfirmed = true;

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
