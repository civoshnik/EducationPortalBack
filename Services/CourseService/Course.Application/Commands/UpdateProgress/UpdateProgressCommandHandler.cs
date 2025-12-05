using Enrollment.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Commands.UpdateProgress
{
    public class UpdateProgressHandler : IRequestHandler<UpdateProgressCommand, SubscriptionEntity>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateProgressHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<SubscriptionEntity> Handle(UpdateProgressCommand request, CancellationToken cancellationToken)
        {
            var subscription = await _unitOfWork.Subscriptions
                .FirstOrDefaultAsync(s => s.UserId == request.UserId && s.CourseId == request.CourseId, cancellationToken)
                ?? throw new KeyNotFoundException("Нет подписки на курс");

            subscription.ProgressPercent = Math.Clamp(request.Progress, 0, 1);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return subscription;
        }
    }
}
