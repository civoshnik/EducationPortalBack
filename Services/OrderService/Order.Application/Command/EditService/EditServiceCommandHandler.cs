using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure;

namespace Order.Application.Command.EditService
{
    public class EditServiceCommandHandler : IRequestHandler<EditServiceCommand>
    {
        private readonly EducationPortalDbContext _dbContext;

        public EditServiceCommandHandler(EducationPortalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(EditServiceCommand request, CancellationToken cancellationToken)
        {
            await _dbContext.Database.ExecuteSqlRawAsync(
                "SELECT edit_service({0}, {1}, {2})",
                request.ServiceId, request.Name, request.Price);
        }
    }
}

