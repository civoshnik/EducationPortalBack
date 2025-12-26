using Auth.Application.Commands.SetAdmin;
using Auth.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Commands.SetStudent
{
    public class SetStudentCommandHandler : IRequestHandler<SetStudentCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SetStudentCommandHandler(IUnitOfWork unitOfWOrk)
        {
            _unitOfWork = unitOfWOrk;
        }

        public async Task Handle(SetStudentCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.SingleOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken)
                ?? throw new Exception($"Пользователь с ID {request.UserId} не найден");

            user.Role = UserRole.Ученик;
            user.ModifiedAt = DateTime.UtcNow;
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
