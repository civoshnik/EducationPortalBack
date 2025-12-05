using Course.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;

namespace Course.Application.Queries.GetLessonForAdmin
{
    public class GetLessonForAdminQueryHandler : IRequestHandler<GetLessonForAdminQuery, LessonEntity>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetLessonForAdminQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        }
        public async Task<LessonEntity> Handle(GetLessonForAdminQuery request, CancellationToken cancellationToken)
        {
            var targetLesson = await _unitOfWork.Lessons.FirstOrDefaultAsync(l => l.LessonId == request.LessonId, cancellationToken)
                ?? throw new Exception($"Урок с ID {request.LessonId} не найден!");

            return targetLesson;
        }
    }
}
