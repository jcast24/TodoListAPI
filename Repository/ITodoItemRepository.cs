using TodoApi.Models;

namespace TodoApi.Repository;

public interface ITodoItemRepository
{
    Task<TodoItem?> GetTodoItemByIdAsync(int id);
    Task<IEnumerable<TodoItem>> GetAllTodoItemsAsync();

    Task<TodoItem> AddTodoAsync(TodoItem todo);
}