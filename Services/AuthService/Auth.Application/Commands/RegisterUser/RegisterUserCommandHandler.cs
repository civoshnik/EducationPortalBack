using Auth.Domain.Models;
using MediatR;
using Shared.Application.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Auth.Application.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (request.Login == null)
            {
                throw new Exception("Заполните");
            }

            if (request.Password == null)
            {
                throw new Exception("Заполните");
            }

            if (request.FirstName == null)
            {
                throw new Exception("Заполните");
            }

            if (request.LastName == null)
            {
                throw new Exception("Заполните");
            }

            if (request.Phone == null)
            {
                throw new Exception("Заполните");
            }

            if (request.Email == null)
            {
                throw new Exception("Заполните");
            }

            UserEntity user = new UserEntity();
            user.Login = request.Login;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Phone = request.Phone;
            user.Email = request.Email;
            user.PasswordHash = HashPassword(request.Password);
            user.CreatedAt = DateTime.UtcNow;
            user.Role = UserRole.Ученик;

            await _unitOfWork.Users.AddAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        }
        public static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
