using Course.Application.Queries.GetCourseList;
using Course.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;
using Shared.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Queries.GetPaginatedLessonList
{
    public class GetPaginatedLessonListQueryHandler : IRequestHandler<GetPaginatedLessonListQuery, PaginatedResult<LessonEntity>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPaginatedLessonListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResult<LessonEntity>> Handle(GetPaginatedLessonListQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.Lessons.OrderByDescending(c => c.CreatedAt);

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedResult<LessonEntity>
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }
}
