namespace TodoApi.Models;

public class TodoItem
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public bool IsCompleted { get; set; }

    // Foreign Key to user
    public int UserId { get; set; }

    // Navigation property back to the user
    public User? User { get; set; }

}
