using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;

namespace Course.Application.Commands.EditCourse
{
    public class EditCourseCommandHandler : IRequestHandler<EditCourseCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditCourseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(EditCourseCommand request, CancellationToken cancellationToken)
        {
            var targetCourse = await _unitOfWork.Courses.FirstOrDefaultAsync(c => c.CourseId == request.CourseId, cancellationToken)
                ?? throw new Exception($"Курс с ID {request.CourseId} не найден!");

            targetCourse.DurationHours = request.DurationHours;
            targetCourse.Creator = request.Creator;
            targetCourse.IsPublished = request.IsPublished;
            targetCourse.Level = request.Level;
            targetCourse.Name = request.Name;
            targetCourse.Category = request.Category;
            targetCourse.ModifiedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
