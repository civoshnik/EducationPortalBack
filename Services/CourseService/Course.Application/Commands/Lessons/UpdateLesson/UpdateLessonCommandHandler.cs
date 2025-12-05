using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;

namespace Course.Application.Commands.Lessons.UpdateLesson
{
    public class UpdateLessonCommandHandler : IRequestHandler<UpdateLessonCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateLessonCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
        {
            var targetCourse = await _unitOfWork.Lessons.FirstOrDefaultAsync(l => l.LessonId == request.LessonId, cancellationToken)
                ?? throw new Exception($"Урок с ID {request.LessonId} не найден");

            targetCourse.ModifiedAt = DateTime.UtcNow;
            targetCourse.Name = request.Name;
            targetCourse.CourseId = request.CourseId;
            targetCourse.VideoUrl = request.VideoURL;
            targetCourse.Content = request.Content;

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
