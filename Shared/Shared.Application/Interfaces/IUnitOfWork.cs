using Auth.Domain.Models;
using Course.Domain.Models;
using Enrollment.Domain.Models;
using Result.Domain.Models;
using Order.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Shared.Application.Interfaces
{
    public interface IUnitOfWork
    {
        DbSet<UserEntity> Users { get; }
        DbSet<CourseEntity> Courses { get; }
        DbSet<LessonEntity> Lessons { get; }
        DbSet<TestEntity> Tests { get; }
        DbSet<QuestionEntity> Questions { get; }
        DbSet<QuestionAnswerEntity> QuestionAnswers { get; }
        DbSet<ResultEntity> Results { get; }
        DbSet<StudentAnswerEntity> StudentAnswers { get; }
        DbSet<EnrollmentEntity> Enrollments { get; }
        DbSet<OrderEntity> Orders { get; }
        DbSet<ServiceEntity> Services { get; }
        DbSet<OrderServiceEntity> OrderServices { get; }

        Task<int> SaveChangesAsync(CancellationToken cancelletionToken = default);
    }
}
