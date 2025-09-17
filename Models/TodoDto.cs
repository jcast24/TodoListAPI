using Microsoft.Identity.Client;

namespace TodoApi.Models;

public class TodoDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }

    public bool IsCompleted { get; set; }
}