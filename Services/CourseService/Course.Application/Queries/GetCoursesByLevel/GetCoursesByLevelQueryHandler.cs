using Course.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Queries.GetCoursesByLevel
{
    public class GetCoursesByLevelHandler : IRequestHandler<GetCoursesByLevelQuery, IEnumerable<CourseEntity>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetCoursesByLevelHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CourseEntity>> Handle(GetCoursesByLevelQuery request, CancellationToken cancelletionToken)
        {
            return await _unitOfWork.Courses
                .Where(c => c.Level == request.Level && c.IsPublished)
                .ToListAsync(cancelletionToken);
        }
    }
}
