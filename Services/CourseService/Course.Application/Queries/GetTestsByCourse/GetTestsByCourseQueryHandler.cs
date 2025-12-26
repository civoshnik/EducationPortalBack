using Course.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Queries.GetTestsByCourse
{

    public class GetTestsByCourseQueryHandler : IRequestHandler<GetTestsByCourseQuery, List<TestEntity>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetTestsByCourseQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<List<TestEntity>> Handle(GetTestsByCourseQuery request, CancellationToken ct)
        {
            var lessonIds = await _unitOfWork.Lessons
                .Where(l => l.CourseId == request.CourseId)
                .Select(l => l.LessonId)
                .ToListAsync(ct);

            return await _unitOfWork.Tests
                .Where(t => lessonIds.Contains(t.LessonId))
                .ToListAsync(ct);
        }
    }


}
