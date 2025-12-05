using Course.Application.Commands.DeleteCourseCommand;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;

namespace Course.Application.Commands.DeleteCourseForAdmin
{
    public class DeleteCourseForAdminCommandHandler : IRequestHandler<DeleteCourseForAdminCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCourseForAdminCommandHandler(IUnitOfWork unitOfWork) 
        { 
            _unitOfWork = unitOfWork ?? throw new Exception(nameof(unitOfWork));
        }
        public async Task Handle(DeleteCourseForAdminCommand request, CancellationToken cancellationToken)
        {
            var targetCorse = await _unitOfWork.Courses.FirstOrDefaultAsync(c => c.CourseId == request.CourseId, cancellationToken)
                ?? throw new Exception($"Курс с ID {request.CourseId} не найден!");

           _unitOfWork.Courses.Remove(targetCorse);
           await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
