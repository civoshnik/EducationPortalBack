public class BlacklistUserDto
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Login { get; set; } = "";
    public string Email { get; set; } = "";
    public int Role { get; set; }
    public string Phone { get; set; } = "";
    public DateTimeOffset BlacklistedAt { get; set; }
}
