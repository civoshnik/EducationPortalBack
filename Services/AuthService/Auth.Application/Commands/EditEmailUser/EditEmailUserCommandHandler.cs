using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Auth.Application.Commands.EditEmailUser
{
    public class EditEmailUserCommandHandler : IRequestHandler<EditEmailUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditEmailUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(EditEmailUserCommand request, CancellationToken cancellationToken)
        {
            var targetUser = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.UserId == request.UserID, cancellationToken)
                ?? throw new Exception($"Пользователь с {request.UserID} не найден!");

            var regexEmail = new Regex(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$");

            if (!regexEmail.IsMatch(request.Email))
                throw new Exception("Неверный формат email!");

            targetUser.Email = request.Email;
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
