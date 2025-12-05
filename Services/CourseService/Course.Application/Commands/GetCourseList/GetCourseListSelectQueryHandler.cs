using Course.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Commands.GetCourseList
{
    public class GetCourseListSelectQueryHandler : IRequestHandler<GetCourseListSelectQuery, List<CourseEntity>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCourseListSelectQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<List<CourseEntity>> Handle(GetCourseListSelectQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Courses
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync(cancellationToken);
        }
    }
}
