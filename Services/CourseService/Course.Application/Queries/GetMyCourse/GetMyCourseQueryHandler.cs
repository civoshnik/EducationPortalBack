using Course.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Queries.GetMyCourse
{
    public class GetMyCourseHandler : IRequestHandler<GetMyCourseQuery, MyCourseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetMyCourseHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<MyCourseDto> Handle(GetMyCourseQuery request, CancellationToken cancellationToken)
        {
            var subscription = await _unitOfWork.Subscriptions
                .FirstOrDefaultAsync(s => s.UserId == request.UserId, cancellationToken);

            if (subscription == null)
                throw new KeyNotFoundException("Нет активной подписки на курс");

            var course = await _unitOfWork.Courses
                .FirstAsync(c => c.CourseId == subscription.CourseId, cancellationToken);

            return new MyCourseDto
            {
                Course = course,
                ProgressPercent = subscription.ProgressPercent
            };
        }

    }
}

