using Course.Application.Queries.GetCourseList;
using Course.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Queries.GetPaginatedLessonList
{
    public class GetPaginatedLessonListQueryHandler : IRequestHandler<GetPaginatedLessonListQuery, List<LessonEntity>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPaginatedLessonListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<LessonEntity>> Handle(GetPaginatedLessonListQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Lessons
                .OrderByDescending(c => c.CreatedAt)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);
        }
    }
}
