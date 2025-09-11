using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models;

/* 
    Database context is the main class that coordinates EF functionality for a data model. This 
    class is created by deriving from the Microsoft.EntityFrameworkCore.DbContext class. 
*/
public class TodoContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<TodoItem> TodoItems { get; set; } = null!;

    public TodoContext(DbContextOptions<TodoContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.TodoItems)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
