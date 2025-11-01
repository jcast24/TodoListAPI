namespace TodoApi.Models;

public class User
{
    public int Id { get; set; } // Primary key
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty; // HashedPassword and not plain text

    // Store refresh token
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }

    // Navigation property for related todo items
    public List<TodoItem> TodoItems { get; set; } = new();

    public string Role { get; set; } = string.Empty;
}
