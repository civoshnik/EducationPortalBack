using MediatR;
using Order.Domain.Models;
using Shared.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Command.CreateService
{
    public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateServiceCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new Exception(nameof(unitOfWork));
        }
        public async Task Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            var service = new ServiceEntity 
            {
                ServiceId = Guid.NewGuid(),
                Name = request.Name,
                Price = request.Price,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow 
            };
            await _unitOfWork.Services.AddAsync(service, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
