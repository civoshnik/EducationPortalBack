using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;
using System.Text.RegularExpressions;

namespace Auth.Application.Commands.EditPhoneUser
{
    public class EditPhoneUserCommandHandler : IRequestHandler<EditPhoneUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditPhoneUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task Handle(EditPhoneUserCommand request, CancellationToken cancellationToken)
        {
            var targetUser = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken)
                ?? throw new Exception($"Пользователь с ID {request.UserId} не найден!");

            var regex = new Regex(@"^\+375(17|25|29|33|44)\d{7}$");

            if (!regex.IsMatch(request.Phone))
                throw new Exception("Неверный формат номера телефона Беларуси!");

            targetUser.Phone = request.Phone;
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
