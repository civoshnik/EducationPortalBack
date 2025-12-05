using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Commands.Lessons.DeleteLesson
{
    public class DeleteLessonCommandHandler : IRequestHandler<DeleteLessonCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLessonCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
        {
            var targetLesson = await _unitOfWork.Lessons.FirstOrDefaultAsync(l => l.LessonId == request.LessonId, cancellationToken)
                ?? throw new ArgumentNullException($"Урок с ID {request.LessonId} не найден!");

            _unitOfWork.Lessons.Remove(targetLesson);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
