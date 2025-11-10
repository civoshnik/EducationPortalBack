using Microsoft.EntityFrameworkCore;
using Auth.Domain.Models;
using Course.Domain.Models;
using Enrollment.Domain.Models;
using Result.Domain.Models;
using Order.Domain.Models;

namespace Shared.Infrastructure
{
    public class EducationPortalDbContext : DbContext
    {
        public EducationPortalDbContext(DbContextOptions<EducationPortalDbContext> options)
            : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CourseEntity> Courses { get; set; }
        public DbSet<LessonEntity> Lessons { get; set; }
        public DbSet<TestEntity> Tests { get; set; }
        public DbSet<QuestionEntity> Questions { get; set; }
        public DbSet<QuestionAnswerEntity> QuestionAnswers { get; set; }
        public DbSet<ResultEntity> Results { get; set; }
        public DbSet<StudentAnswerEntity> StudentAnswers { get; set; }
        public DbSet<EnrollmentEntity> Enrollments { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<ServiceEntity> Services { get; set; }
        public DbSet<OrderServiceEntity> OrderServices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasKey(u => u.UserId);
            modelBuilder.Entity<CourseEntity>().HasKey(c => c.CourseId);
            modelBuilder.Entity<LessonEntity>().HasKey(l => l.LessonId);
            modelBuilder.Entity<TestEntity>().HasKey(t => t.TestId);
            modelBuilder.Entity<QuestionEntity>().HasKey(q => q.QuestionId);
            modelBuilder.Entity<QuestionAnswerEntity>().HasKey(qa => qa.QuestionAnswerId);
            modelBuilder.Entity<ResultEntity>().HasKey(r => r.ResultId);
            modelBuilder.Entity<StudentAnswerEntity>().HasKey(sa => sa.StudentAnswerId);
            modelBuilder.Entity<EnrollmentEntity>().HasKey(e => e.EnrollmentId);
            modelBuilder.Entity<OrderEntity>().HasKey(o => o.OrderId);
            modelBuilder.Entity<ServiceEntity>().HasKey(s => s.ServiceId);
            modelBuilder.Entity<OrderServiceEntity>().HasKey(os => os.OrderServiceId);

            // User ↔ Enrollment
            modelBuilder.Entity<UserEntity>()
                .HasMany<EnrollmentEntity>()
                .WithOne()
                .HasForeignKey(e => e.UserId);

            // User ↔ Result
            modelBuilder.Entity<UserEntity>()
                .HasMany<ResultEntity>()
                .WithOne()
                .HasForeignKey(r => r.UserId);

            // User ↔ Order
            modelBuilder.Entity<UserEntity>()
                .HasMany<OrderEntity>()
                .WithOne()
                .HasForeignKey(o => o.UserId);

            // Course ↔ Lesson
            modelBuilder.Entity<CourseEntity>()
                .HasMany<LessonEntity>()
                .WithOne()
                .HasForeignKey(l => l.CourseId);

            // Lesson ↔ Test
            modelBuilder.Entity<LessonEntity>()
                .HasMany<TestEntity>()
                .WithOne()
                .HasForeignKey(t => t.LessonId);

            // Test ↔ Question
            modelBuilder.Entity<TestEntity>()
                .HasMany<QuestionEntity>()
                .WithOne()
                .HasForeignKey(q => q.TestId);

            // Question ↔ QuestionAnswer
            modelBuilder.Entity<QuestionEntity>()
                .HasMany<QuestionAnswerEntity>()
                .WithOne()
                .HasForeignKey(a => a.QuestionId);

            // Test ↔ Result
            modelBuilder.Entity<TestEntity>()
                .HasMany<ResultEntity>()
                .WithOne()
                .HasForeignKey(r => r.TestId);

            // Result ↔ StudentAnswer
            modelBuilder.Entity<ResultEntity>()
                .HasMany<StudentAnswerEntity>()
                .WithOne()
                .HasForeignKey(sa => sa.ResultId);

            // Question ↔ StudentAnswer
            modelBuilder.Entity<QuestionEntity>()
                .HasMany<StudentAnswerEntity>()
                .WithOne()
                .HasForeignKey(sa => sa.QuestionId);

            // QuestionAnswer ↔ StudentAnswer
            modelBuilder.Entity<QuestionAnswerEntity>()
                .HasMany<StudentAnswerEntity>()
                .WithOne()
                .HasForeignKey(sa => sa.QuestionAnswerId);

            // Order ↔ OrderService
            modelBuilder.Entity<OrderEntity>()
                .HasMany<OrderServiceEntity>()
                .WithOne()
                .HasForeignKey(os => os.OrderId);

            // Service ↔ OrderService
            modelBuilder.Entity<ServiceEntity>()
                .HasMany<OrderServiceEntity>()
                .WithOne()
                .HasForeignKey(os => os.ServiceId);
        }
    }
}
