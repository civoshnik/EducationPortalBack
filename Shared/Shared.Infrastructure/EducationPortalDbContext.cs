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
        public DbSet<SubscriptionEntity> Subscriptions { get; set; }
        public DbSet<LearningProgressEntity> LearningProgress { get; set; }
        public DbSet<TestingProgressEntity> TestingProgresses { get; set; }
        public DbSet<StudentAnswerEntity> StudentAnswers { get; set; }
        public DbSet<BlacklistEntity> Blacklist { get; set; }

        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<ServiceEntity> Services { get; set; }
        public DbSet<OrderServiceEntity> OrderServices { get; set; }
        public DbSet<CartItemEntity> CartItems { get; set; }
        public DbSet<EmailConfirmTokenEntity> EmailConfirmTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // === Keys ===
            modelBuilder.Entity<UserEntity>().HasKey(u => u.UserId);
            modelBuilder.Entity<CourseEntity>().HasKey(c => c.CourseId);
            modelBuilder.Entity<LessonEntity>().HasKey(l => l.LessonId);
            modelBuilder.Entity<TestEntity>().HasKey(t => t.TestId);
            modelBuilder.Entity<QuestionEntity>().HasKey(q => q.QuestionId);
            modelBuilder.Entity<QuestionAnswerEntity>().HasKey(qa => qa.QuestionAnswerId);

            modelBuilder.Entity<SubscriptionEntity>().HasKey(s => s.SubscriptionId);
            modelBuilder.Entity<LearningProgressEntity>().HasKey(lp => new { lp.LessonId, lp.SubscriptionId });

            modelBuilder.Entity<TestingProgressEntity>().HasKey(tp => tp.TestingProgressId);
            modelBuilder.Entity<StudentAnswerEntity>().HasKey(sa => sa.StudentAnswerId);

            modelBuilder.Entity<OrderEntity>().HasKey(o => o.OrderId);
            modelBuilder.Entity<ServiceEntity>().HasKey(s => s.ServiceId);
            modelBuilder.Entity<OrderServiceEntity>().HasKey(os => new { os.OrderId, os.ServiceId });

            modelBuilder.Entity<CartItemEntity>().HasKey(ci => ci.CartItemId);
            modelBuilder.Entity<BlacklistEntity>().HasKey(b => b.UserId);

            // === Relationships ===

            // User ↔ Subscription
            modelBuilder.Entity<UserEntity>()
                .HasMany<SubscriptionEntity>()
                .WithOne()
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Course ↔ Subscription
            modelBuilder.Entity<CourseEntity>()
                .HasMany<SubscriptionEntity>()
                .WithOne()
                .HasForeignKey(s => s.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Course ↔ Lesson
            modelBuilder.Entity<CourseEntity>()
                .HasMany<LessonEntity>()
                .WithOne()
                .HasForeignKey(l => l.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Lesson ↔ Test
            modelBuilder.Entity<LessonEntity>()
                .HasMany<TestEntity>()
                .WithOne()
                .HasForeignKey(t => t.LessonId)
                .OnDelete(DeleteBehavior.Cascade);

            // Test ↔ Question
            modelBuilder.Entity<TestEntity>()
                .HasMany<QuestionEntity>()
                .WithOne()
                .HasForeignKey(q => q.TestId)
                .OnDelete(DeleteBehavior.Cascade);

            // Question ↔ QuestionAnswer
            modelBuilder.Entity<QuestionEntity>()
                .HasMany<QuestionAnswerEntity>()
                .WithOne()
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Subscription ↔ LearningProgress ↔ Lesson
            modelBuilder.Entity<LearningProgressEntity>()
                .HasOne<SubscriptionEntity>()
                .WithMany()
                .HasForeignKey(lp => lp.SubscriptionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LearningProgressEntity>()
                .HasOne<LessonEntity>()
                .WithMany()
                .HasForeignKey(lp => lp.LessonId)
                .OnDelete(DeleteBehavior.Cascade);

            // Subscription ↔ TestingProgress ↔ Test
            modelBuilder.Entity<TestEntity>()
                .HasMany<TestingProgressEntity>()
                .WithOne()
                .HasForeignKey(tp => tp.TestId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SubscriptionEntity>()
                .HasMany<TestingProgressEntity>()
                .WithOne()
                .HasForeignKey(tp => tp.SubscriptionId)
                .OnDelete(DeleteBehavior.Cascade);

            // TestingProgress ↔ StudentAnswer
            modelBuilder.Entity<TestingProgressEntity>()
                .HasMany<StudentAnswerEntity>()
                .WithOne()
                .HasForeignKey(sa => sa.TestingProgressId)
                .OnDelete(DeleteBehavior.Cascade);

            // Question ↔ StudentAnswer
            modelBuilder.Entity<QuestionEntity>()
                .HasMany<StudentAnswerEntity>()
                .WithOne()
                .HasForeignKey(sa => sa.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            // QuestionAnswer ↔ StudentAnswer
            modelBuilder.Entity<QuestionAnswerEntity>()
                .HasMany<StudentAnswerEntity>()
                .WithOne()
                .HasForeignKey(sa => sa.QuestionAnswerId)
                .OnDelete(DeleteBehavior.Cascade);

            // User ↔ Order
            modelBuilder.Entity<UserEntity>()
                .HasMany<OrderEntity>()
                .WithOne()
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Order ↔ OrderService
            modelBuilder.Entity<OrderEntity>()
                .HasMany<OrderServiceEntity>()
                .WithOne()
                .HasForeignKey(os => os.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Service ↔ OrderService
            modelBuilder.Entity<ServiceEntity>()
                .HasMany<OrderServiceEntity>()
                .WithOne()
                .HasForeignKey(os => os.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);

            // User ↔ CartItem
            modelBuilder.Entity<UserEntity>()
                .HasMany<CartItemEntity>()
                .WithOne()
                .HasForeignKey(ci => ci.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Service ↔ CartItem
            modelBuilder.Entity<ServiceEntity>()
                .HasMany<CartItemEntity>()
                .WithOne()
                .HasForeignKey(ci => ci.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Blacklist ↔ User
            modelBuilder.Entity<BlacklistEntity>()
                .HasOne<UserEntity>()
                .WithOne()
                .HasForeignKey<BlacklistEntity>(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EmailConfirmTokenEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.User)
                      .WithMany()
                      .HasForeignKey(e => e.UserId);
            });
        }
    }
}
