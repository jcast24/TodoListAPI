using TodoApi.Models;
using TodoApi.Repository;

namespace TodoApi.Services;

public class TodoItemService : ITodoItemService
{
    private readonly ITodoItemRepository _repository;

    public TodoItemService(ITodoItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<TodoItem?> GetTodoItemAsync(int id)
    {
        return await _repository.GetTodoItemByIdAsync(id);
    }

    public async Task<IEnumerable<TodoItem>> GetAllTodoItems()
    {
        return await _repository.GetAllTodoItemsAsync();
    }

    public async Task<TodoItem> CreateTodoAsync(TodoItem todo)
    {
        return await _repository.AddTodoAsync(todo);
    }

    public async Task<bool> UpdateTodoAsync(TodoItem todo)
    {
        return await _repository.UpdateTodoAsync(todo);
    }

    public async Task<bool> DeleteTodoAsync(int id)
    {
        return await _repository.DeleteTodoAsync(id);
    }
}
