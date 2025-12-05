namespace Auth.Domain.Models
{
    public class BlacklistEntity
    {
        public Guid UserId { get; set; }
        public DateTimeOffset BlacklistedAt { get; set; }
    }
}
