using TodoApi.Models;

namespace TodoApi.Services;

public interface ITodoItemService
{
    Task<TodoItem?> GetTodoItemAsync(int id);
    Task<IEnumerable<TodoItem>> GetAllUserTodoItemsAsync(int userId);

    Task<TodoItem> CreateTodoAsync(int userId, TodoItem todo);

    Task<TodoItem?> UpdateTodoAsync(int userId, int todoId, TodoItem todo);

    Task<bool> DeleteTodoAsync(int userId, int todoId);
}