using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Services;

public class TodoItemService : ITodoItemService
{
    private readonly TodoContext _context;

    public TodoItemService(TodoContext context)
    {
        _context = context;
    }

    public async Task<TodoItem?> GetTodoItemAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TodoItem>> GetAllUserTodoItemsAsync(int userId)
    {
        var todos = await _context.TodoItems.Where(t => t.UserId == userId).ToListAsync();
        return todos;
    }

    public async Task<TodoItem> CreateTodoAsync(int userId, TodoItem todo)
    {
        var newTodo = new TodoItem
        {
            Title = todo.Title,
            Description = todo.Description,
            IsCompleted = false,
            UserId = userId
        };

        _context.TodoItems.Add(newTodo);
        await _context.SaveChangesAsync();

        return newTodo;
    }

    public async Task<TodoItem?> UpdateTodoAsync(int todoId, int userId, TodoItem todo)
    {
        var getTodo = await _context.TodoItems.FirstOrDefaultAsync(t => t.Id == todoId && t.UserId == userId);

        if (getTodo == null)
        {
            return null;
        }

        getTodo.Title = todo.Title;
        getTodo.Description = todo.Description;
        getTodo.IsCompleted = todo.IsCompleted;

        await _context.SaveChangesAsync();
        return getTodo;
    }

    public async Task<bool> DeleteTodoAsync(int userId, int todoId)
    {
        var chosenTodo = await _context.TodoItems.FirstOrDefaultAsync(t => t.Id == todoId && t.UserId == userId);

        if (chosenTodo == null)
        {
            return false;
        }

        _context.Remove(chosenTodo);
        await _context.SaveChangesAsync();
        return true;
    }
}
