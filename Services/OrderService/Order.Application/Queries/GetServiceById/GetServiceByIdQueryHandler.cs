using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Domain.Models;
using Shared.Application.Interfaces;

namespace Order.Application.Queries.GetServiceById
{
    public class GetServiceByIdQueryHandler : IRequestHandler<GetServiceByIdQuery, ServiceEntity>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetServiceByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new Exception(nameof(unitOfWork));
        }
        public async Task<ServiceEntity> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
        {
            var targetService = await _unitOfWork.Services.SingleOrDefaultAsync(s => s.ServiceId == request.ServiceId, cancellationToken)
                ?? throw new Exception($"Услуга с ID {request.ServiceId} не найдена!");

            return targetService;
        }
    }
}
