using Course.Domain.Models;
using MediatR;
using Shared.Application.Interfaces;

namespace Course.Application.Commands.Lessons.CreateLesson
{
    public class CreateLessonCommandHandler : IRequestHandler<CreateLessonCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateLessonCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task Handle(CreateLessonCommand request, CancellationToken cancellationToken)
        {
            var lesson = new LessonEntity
            {
                CourseId = request.CourseId,
                VideoUrl = request.VideoURL,
                Content = request.Content,
                Name = request.Name,
                CreatedAt = DateTime.UtcNow,
            };

            await _unitOfWork.Lessons.AddAsync(lesson, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
