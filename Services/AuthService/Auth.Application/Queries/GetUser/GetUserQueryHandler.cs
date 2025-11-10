using Auth.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;

namespace Auth.Application.Queries.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserEntity>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetUserQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserEntity> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var targetUser = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.UserId == request.Id, cancellationToken)
                ?? throw new Exception("Пользователь не найден");

            return targetUser;
        }
    }
}
