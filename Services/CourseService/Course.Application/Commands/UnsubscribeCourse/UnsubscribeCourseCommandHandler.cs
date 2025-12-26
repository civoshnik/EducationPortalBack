using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Commands.UnsubscribeCourse
{

    public class UnsubscribeCourseCommandHandler : IRequestHandler<UnsubscribeCourseCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UnsubscribeCourseCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task Handle(UnsubscribeCourseCommand request, CancellationToken ct)
        {
            var subscription = await _unitOfWork.Subscriptions
                .SingleOrDefaultAsync(s => s.UserId == request.UserId && s.CourseId == request.CourseId, ct)
                ?? throw new Exception("Подписка не найдена");

            _unitOfWork.Subscriptions.Remove(subscription);
            await _unitOfWork.SaveChangesAsync(ct);
        }
    }

}
