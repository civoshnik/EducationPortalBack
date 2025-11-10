using Course.Domain.Models;
using MediatR;
using Shared.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Commands.CreateCourse
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCourseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = new CourseEntity
            {
                CourseId = Guid.NewGuid(),
                Name = request.Name,
                Category = request.Category,
                Level = request.Level,
                DurationHours = request.DurationHours,
                Creator = request.Creator,
                IsPublished = request.IsPublished,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Courses.AddAsync(course, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
