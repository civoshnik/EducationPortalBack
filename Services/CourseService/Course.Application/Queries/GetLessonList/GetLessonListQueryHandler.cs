using Course.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;

namespace Course.Application.Queries.GetLessonListQuery
{
    public class GetLessonListQueryHandler : IRequestHandler<GetLessonListQuery, List<LessonEntity>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetLessonListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<List<LessonEntity>> Handle(GetLessonListQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Lessons
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync(cancellationToken);
        }
    }
}
