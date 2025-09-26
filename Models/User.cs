namespace TodoApi.Models;

public class User
{
    public int Id { get; set; } // Primary key
    public string Username { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string PasswordHash { get; set; } = String.Empty; // HashedPassword and not plain text

    // Store refresh token
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }

    // Navigation property for related todo items
    public List<TodoItem> TodoItems { get; set; } = new();
}
