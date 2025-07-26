using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    /*
        Class is marked with [ApiController] attribute that indicates that the controller responds to the web API requests.
        Uses DI to inject the database context into the controller. The DB context is used in each of the CRUD methods in the controller.
    */
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemService _todoService;

        public TodoItemsController(ITodoItemService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            var todo = await _todoService.GetAllTodoItems();
            return Ok(todo);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTodoItem(int id)
        {
            var todo = await _todoService.GetTodoItemAsync(id);

            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todo)
        {
            var created = await _todoService.CreateTodoAsync(todo);
            return CreatedAtAction(nameof(GetTodoItem), new { id = created.Id }, created);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTodoItem(int id, TodoItem todo)
        {
            if (id != todo.Id)
            {
                return BadRequest("Id mismatch!");
            }

            var updated = await _todoService.UpdateTodoAsync(todo);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            var deleted = await _todoService.DeleteTodoAsync(id);
            return deleted ? NoContent() : NotFound();
        }

        // private bool TodoItemExists(long id)
        // {
        //     return _context.TodoItems.Any(e => e.Id == id);
        // }

        // Basically converts the TodoItem model class to the DTO without needing to pass
        // in the "Secret" field from the original class.
        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete,
            };
    }
}
