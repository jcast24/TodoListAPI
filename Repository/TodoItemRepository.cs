using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApi.Repository;

namespace TodoApi.Repository;

public class TodoItemRepository : ITodoItemRepository
{
    private readonly TodoContext _context;

    public TodoItemRepository(TodoContext context)
    {
        _context = context;
    }
    public async Task<TodoItem?> GetTodoItemByIdAsync(int id)
    {
        return await _context.TodoItems.FindAsync(id);
    }

    public async Task<IEnumerable<TodoItem>> GetAllTodoItemsAsync()
    {
        return await _context.TodoItems.ToListAsync();
    }

    public async Task<TodoItem> AddTodoAsync(TodoItem todo)
    {
        _context.TodoItems.Add(todo);
        await _context.SaveChangesAsync();
        return todo;
    }
}