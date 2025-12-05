using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Domain.Models;
using Shared.Application.Interfaces;

namespace Order.Application.Queries
{
    public class GetPaginatedServicesQueryHandler : IRequestHandler<GetPaginatedServicesQuery, List<ServiceEntity>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPaginatedServicesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ServiceEntity>> Handle(GetPaginatedServicesQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Services
                .OrderBy(s => s.Name)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);
        }
    }
}
