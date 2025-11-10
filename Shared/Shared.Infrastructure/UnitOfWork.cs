using Shared.Application.Interfaces;
using Auth.Domain.Models;
using Course.Domain.Models;
using Enrollment.Domain.Models;
using Result.Domain.Models;
using Order.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Shared.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EducationPortalDbContext _context;

        public UnitOfWork(EducationPortalDbContext context)
        {
            _context = context;
        }

        public DbSet<UserEntity> Users => _context.Users;
        public DbSet<CourseEntity> Courses => _context.Courses;
        public DbSet<LessonEntity> Lessons => _context.Lessons;
        public DbSet<TestEntity> Tests => _context.Tests;
        public DbSet<QuestionEntity> Questions => _context.Questions;
        public DbSet<QuestionAnswerEntity> QuestionAnswers => _context.QuestionAnswers;
        public DbSet<ResultEntity> Results => _context.Results;
        public DbSet<StudentAnswerEntity> StudentAnswers => _context.StudentAnswers;
        public DbSet<EnrollmentEntity> Enrollments => _context.Enrollments;
        public DbSet<OrderEntity> Orders => _context.Orders;
        public DbSet<ServiceEntity> Services => _context.Services;
        public DbSet<OrderServiceEntity> OrderServices => _context.OrderServices;

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => _context.SaveChangesAsync(cancellationToken);
    }
}
