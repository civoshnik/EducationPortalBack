using Course.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;
using System.Linq;

namespace Course.Application.Queries.GetLessonCourseForAdmin
{
    public class GetLessonCourseForAdminQueryHandler : IRequestHandler<GetLessonCourseForAdminQuery, List<LessonEntity>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetLessonCourseForAdminQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new Exception(nameof(unitOfWork));
        }

        public async Task<List<LessonEntity>> Handle(GetLessonCourseForAdminQuery request, CancellationToken cancellationToken)
        {
            var lessonList = await _unitOfWork.Lessons.Where(l => l.CourseId == request.CourseId).ToListAsync(cancellationToken)
                ?? throw new Exception($"Уроки с ID курса {request.CourseId} не найдены!");

            return lessonList;
        }
    }
}
