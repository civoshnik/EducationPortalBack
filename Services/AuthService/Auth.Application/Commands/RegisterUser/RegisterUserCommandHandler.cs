using Auth.Domain.Models;
using MediatR;
using Shared.Application.Interfaces;
using Shared.RabbitMQ.Abstractions;
using Shared.RabbitMQ.Events.Auth;
using System.Security.Cryptography;
using System.Text;

namespace Auth.Application.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventBus _eventBus;

        public RegisterUserCommandHandler(IUnitOfWork unitOfWork, IEventBus eventBus)
        {
            _unitOfWork = unitOfWork;
            _eventBus = eventBus;
        }

        public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Login)) throw new Exception("Заполните логин");
            if (string.IsNullOrWhiteSpace(request.Password)) throw new Exception("Заполните пароль");
            if (string.IsNullOrWhiteSpace(request.FirstName)) throw new Exception("Заполните имя");
            if (string.IsNullOrWhiteSpace(request.LastName)) throw new Exception("Заполните фамилию");
            if (string.IsNullOrWhiteSpace(request.Phone)) throw new Exception("Заполните телефон");
            if (string.IsNullOrWhiteSpace(request.Email)) throw new Exception("Заполните email");

            var user = new UserEntity
            {
                Login = request.Login,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Phone = request.Phone,
                Email = request.Email,
                PasswordHash = HashPassword(request.Password),
                CreatedAt = DateTime.UtcNow,
                Role = UserRole.Ученик,
            };

            await _unitOfWork.Users.AddAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var confirmToken = Guid.NewGuid().ToString("N");
            var expiresAt = DateTime.UtcNow.AddHours(24);
            var confirmUrl = $"https://auth.yourapp.com/confirm?userId={user.UserId}&token={confirmToken}";

            var evnt = new UserRegisteredEvent
            {
                UserId = user.UserId,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ConfirmToken = confirmToken,
                ExpiresAt = expiresAt,
                ConfirmUrl = confirmUrl
            };

            await _eventBus.Publish(evnt);

            var tokenEntity = new EmailConfirmTokenEntity
            {
                UserId = user.UserId,
                Token = confirmToken,
                ExpiresAt = expiresAt,
                IsUsed = false,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.EmailConfirmTokens.AddAsync(tokenEntity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
