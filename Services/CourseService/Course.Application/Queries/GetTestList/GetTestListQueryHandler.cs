using Course.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Queries.GetTestList
{
    public class GetTestListQueryHandler : IRequestHandler<GetTestListQuery, List<TestEntity>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTestListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new Exception(nameof(unitOfWork));
        }

        public async Task<List<TestEntity>> Handle(GetTestListQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Tests
               .OrderByDescending(c => c.CreatedAt)
               .ToListAsync(cancellationToken);
        }
    }
}
