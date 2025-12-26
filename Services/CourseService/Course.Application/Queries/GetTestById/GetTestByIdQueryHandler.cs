using Course.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;

namespace Course.Application.Queries.GetTestById
{
    public class GetTestByIdQueryHandler : IRequestHandler<GetTestByIdQuery, TestEntity?>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetTestByIdQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<TestEntity?> Handle(GetTestByIdQuery request, CancellationToken ct)
        {
            return await _unitOfWork.Tests
                .SingleOrDefaultAsync(t => t.TestId == request.TestId, ct);
        }
    }

}
