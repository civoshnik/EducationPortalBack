using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared.Application.Interfaces;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace Auth.Application.Commands.Autorization
{
    public class AutorizationCommandHandler : IRequestHandler<AutorizationCommand, AuthResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AutorizationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new Exception(nameof(AutorizationCommandHandler));
        }

        public async Task<AuthResult> Handle(AutorizationCommand request, CancellationToken cancellationToken)
        {
            var targetUser = await _unitOfWork.Users
                .FirstOrDefaultAsync(u => u.Login == request.Login, cancellationToken)
                ?? throw new Exception("Пользователь не найден");

            if (!VerifyPassword(request.Password, targetUser.PasswordHash))
                throw new Exception("Неверный пароль");

            if (!targetUser.EmailConfirmed)
                throw new Exception("Email не подтверждён");

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, targetUser.Login),
                new Claim(ClaimTypes.NameIdentifier, targetUser.UserId.ToString()),
                new Claim(ClaimTypes.Role, targetUser.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super_secret_key_12345_super_secret_key_12345"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "MyAuthServer",
                audience: "MyAuthClient",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new AuthResult
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserId = targetUser.UserId
            };
        }

        public static bool VerifyPassword(string password, string storedHash)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            var newHash = Convert.ToBase64String(hash);

            return newHash == storedHash;
        }
    }
}
