using Auth.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Commands.SetAdmin
{
    public class SetAdminCommandHandler : IRequestHandler<SetAdminCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SetAdminCommandHandler(IUnitOfWork unitOfWOrk)
        {
            _unitOfWork = unitOfWOrk;
        }

        public async Task Handle(SetAdminCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.SingleOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken)
                ?? throw new Exception($"Пользователь с ID {request.UserId} не найден");

            user.Role = UserRole.Администратор; 
            user.ModifiedAt = DateTime.UtcNow; 
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
