using TodoApi.Models;

namespace TodoApi.Services;

public interface ITodoItemService
{
    Task<TodoItem?> GetTodoItemAsync(int id);
    Task<IEnumerable<TodoItem>> GetAllTodoItems();

    Task<TodoItem> CreateTodoAsync(TodoItem todo);

    Task<bool> UpdateTodoAsync(TodoItem todo);
}