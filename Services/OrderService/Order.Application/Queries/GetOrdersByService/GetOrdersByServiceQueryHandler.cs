using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Domain.Models;
using Shared.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Application.Queries.GetOrdersByService
{
    public class GetOrdersByServiceQueryHandler : IRequestHandler<GetOrdersByServiceQuery, List<OrderEntity>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrdersByServiceQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<OrderEntity>> Handle(GetOrdersByServiceQuery request, CancellationToken cancellationToken)
        {
            var orderIds = await _unitOfWork.OrderServices
                .Where(os => os.ServiceId == request.ServiceId)
                .Select(os => os.OrderId)
                .Distinct()
                .ToListAsync(cancellationToken);

            return await _unitOfWork.Orders
                .Where(o => orderIds.Contains(o.OrderId))
                .ToListAsync(cancellationToken);
        }
    }
}
