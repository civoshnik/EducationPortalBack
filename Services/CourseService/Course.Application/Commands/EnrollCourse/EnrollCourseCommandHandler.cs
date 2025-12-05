using Enrollment.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Domain.Models;
using Shared.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Commands.EnrollCourse
{
    public class EnrollCourseHandler : IRequestHandler<EnrollCourseCommand, SubscriptionEntity>
    {
        private readonly IUnitOfWork _unitOfWork;
        public EnrollCourseHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<SubscriptionEntity> Handle(EnrollCourseCommand request, CancellationToken cancellationToken)
        {
            var existing = await _unitOfWork.Subscriptions
                .FirstOrDefaultAsync(s => s.UserId == request.UserId && s.CourseId == request.CourseId, cancellationToken);

            if (existing != null)
                throw new InvalidOperationException("Пользователь уже подписан на этот курс");

            var subscription = new SubscriptionEntity
            {
                SubscriptionId = Guid.NewGuid(),
                UserId = request.UserId,
                CourseId = request.CourseId,
                SubscribedAt = DateTime.UtcNow,
                ProgressPercent = 0,
                Status = "Active"
            };

            await _unitOfWork.Subscriptions.AddAsync(subscription, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return subscription;
        }
    }
}
