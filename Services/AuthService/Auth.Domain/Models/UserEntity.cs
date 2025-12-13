using System.ComponentModel.DataAnnotations;

namespace Auth.Domain.Models
{
    public enum UserRole
    {
        Администратор,
        Ученик,
        Учитель
    }
    public class UserEntity
    {
        [Key]
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool EmailConfirmed { get; set; } = false;
        public string? EmailConfirmationToken { get; set; }
        public DateTime? EmailConfirmationExpires { get; set; }
        public UserRole Role { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
