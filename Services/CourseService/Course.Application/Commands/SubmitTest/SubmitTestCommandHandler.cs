using Course.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Course.Application.Commands.SubmitTest
{
    public class SubmitTestCommandHandler : IRequestHandler<SubmitTestCommand, SubmitResult>
    {
        private readonly IUnitOfWork _uow;
        public SubmitTestCommandHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<SubmitResult> Handle(SubmitTestCommand request, CancellationToken ct)
        {

            var test = await _uow.Tests
                .SingleOrDefaultAsync(t => t.TestId == request.TestId, ct)
                ?? throw new Exception($"Тест {request.TestId} не найден");


            var questionIds = await _uow.Questions
                .Where(q => q.TestId == request.TestId)
                .Select(q => q.QuestionId)
                .ToListAsync(ct);

            var answers = await _uow.QuestionAnswers
                .Where(a => questionIds.Contains(a.QuestionId))
                .Select(a => new { a.QuestionAnswerId, a.QuestionId, a.IsCorrect })
                .ToListAsync(ct);


            int grade = 0;
            foreach (var dto in request.Answers)
            {
                var isCorrect = answers.Any(a =>
                    a.QuestionId == dto.QuestionId &&
                    a.QuestionAnswerId == dto.AnswerId &&
                    a.IsCorrect);

                if (isCorrect) grade++;
            }

            int assignedGrade = questionIds.Count;
            bool passed = grade >= test.PassingScore;


            var lessonCourseId = await _uow.Lessons
                .Where(l => l.LessonId == test.LessonId)
                .Select(l => l.CourseId)
                .FirstAsync(ct);

            var subscription = await _uow.Subscriptions
                .SingleOrDefaultAsync(s => s.UserId == request.UserId && s.CourseId == lessonCourseId, ct)
                ?? throw new Exception("Подписка не найдена");

            var progress = new TestingProgressEntity
            {
                TestingProgressId = Guid.NewGuid(),
                TestId = test.TestId,
                SubscriptionId = subscription.SubscriptionId,
                Grade = grade,
                AssignedGrade = assignedGrade,
                TimeSpentSeconds = request.TimeSpentSeconds,
                PassedAt = passed ? DateTime.UtcNow : DateTime.MinValue 
            };

            _uow.TestingProgresses.Add(progress);

            var totalTests = await (from t in _uow.Tests
                                    join l in _uow.Lessons on t.LessonId equals l.LessonId
                                    where l.CourseId == lessonCourseId
                                    select t).CountAsync(ct);

            if (totalTests > 0)
            {
                var passedTests = await _uow.TestingProgresses
                    .CountAsync(tp => tp.SubscriptionId == subscription.SubscriptionId && tp.PassedAt != DateTime.MinValue, ct);

                subscription.ProgressPercent = Math.Round((decimal)passedTests / (decimal)totalTests, 2);
                _uow.Subscriptions.Update(subscription);
            }

            await _uow.SaveChangesAsync(ct);

            return new SubmitResult
            {
                Success = true,
                Grade = grade,
                AssignedGrade = assignedGrade,
                Passed = passed
            };
        }
    }
}
