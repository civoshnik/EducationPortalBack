using Course.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;

namespace Course.Application.Queries.GetCourseForAdmin
{
    public class GetCourseForAdminQueryHandler : IRequestHandler<GetCourseForAdminQuery, CourseEntity>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCourseForAdminQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<CourseEntity> Handle(GetCourseForAdminQuery request, CancellationToken cancellationToken)
        {
            var targetCourse = await _unitOfWork.Courses.FirstOrDefaultAsync(c => c.CourseId == request.CourseId, cancellationToken)
                ?? throw new Exception($"Заказ с ID {request.CourseId} не найден!");

            return targetCourse;
        }
    }
}
