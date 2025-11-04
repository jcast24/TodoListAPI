using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    /*
        Class is marked with [ApiController] attribute that indicates that the controller responds to the web API requests.
        Uses DI to inject the database context into the controller. The DB context is used in each of the CRUD methods in the controller.
    */
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemService _todoService;
        private readonly TodoContext _todoContext;

        public TodoItemsController(ITodoItemService todoService, TodoContext todoContext)
        {
            _todoService = todoService;
            _todoContext = todoContext;
        }

        [HttpGet("mytodos")]
        public async Task<IActionResult> GetUserTodos()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            int user = int.Parse(userIdClaim.Value);
            var todos = await _todoService.GetAllUserTodoItemsAsync(user);

            if (todos == null || todos.Count() == 0)
            {
                return NotFound("No todos for this user.");
            }

            return Ok(todos);
        }

        [HttpPost("add")]
        public async Task<IActionResult> CreateUserTodo([FromBody] TodoItem todoItem)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            int userId = int.Parse(userIdClaim.Value);

            var createdTodo = await _todoService.CreateTodoAsync(userId, todoItem);

            return Ok(createdTodo);
        }

        // update entire todo
        [HttpPut("update")]
        public async Task<IActionResult> UpdateFullTask([FromBody] TodoItem todoItem)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            int userId = int.Parse(userIdClaim.Value);

            var updatedTodo = await _todoService.UpdateTodoAsync(userId, todoItem);
            if (updatedTodo == null)
            {
                return NotFound("Todo not found.");
            }

            return Ok(updatedTodo);
        }

        // update isCompleted 
        [HttpPatch("completed")]
        public async Task<IActionResult> UpdateCompletedTask([FromBody] TodoItem todoItem)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            int userId = int.Parse(userIdClaim.Value);

            var updatedTodo = await _todoService.PatchTodoCompleteAsync(userId, todoItem);

            if (updatedTodo == null)
            {
                return NotFound();
            }

            return Ok(updatedTodo);
        }

        
        // [HttpDelete("delete/{id}")]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteTask([FromBody] TodoItem todo)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            int userId = int.Parse(userIdClaim.Value);

            var deleted = await _todoService.DeleteTodoAsync(userId, todo.Id);

            if (!deleted)
            {
                return NotFound("Todo not found");
            }

            return Ok("Todo deleted.");
        }

        
    }
}
