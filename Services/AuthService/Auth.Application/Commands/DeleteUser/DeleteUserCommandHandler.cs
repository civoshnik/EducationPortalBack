using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;

namespace Auth.Application.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var targetUser = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken)
                ?? throw new Exception("Пользователь не найден");

            _unitOfWork.Users.Remove(targetUser);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
