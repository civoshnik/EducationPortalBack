using Course.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Queries.GetUserCourseForAdmin
{
    public class GetUserCourseForAdminQueryHandler : IRequestHandler<GetUserCourseForAdminQuery, List<CourseEntity>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserCourseForAdminQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<List<CourseEntity>> Handle(GetUserCourseForAdminQuery request, CancellationToken cancellationToken)
        {
            var subscriptions = await _unitOfWork.Subscriptions
                .Where(s => s.UserId == request.UserId)
                .ToListAsync(cancellationToken);

            var courseIds = subscriptions.Select(s => s.CourseId).ToList();

            var courses = await _unitOfWork.Courses
                .Where(c => courseIds.Contains(c.CourseId))
                .ToListAsync(cancellationToken);

            return courses;
        }
    }
}
