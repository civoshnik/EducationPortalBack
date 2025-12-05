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

namespace Course.Application.Queries.GetCourseList
{
    public class GetCourseListQueryHandler : IRequestHandler<GetCourseListQuery, PaginatedResult<CourseEntity>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCourseListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResult<CourseEntity>> Handle(GetCourseListQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.Courses.OrderByDescending(c => c.CreatedAt);

            var items = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var totalCount = await query.CountAsync(cancellationToken);

            return new PaginatedResult<CourseEntity>
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }
}
