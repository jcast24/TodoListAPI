using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            int? user = int.Parse(userIdClaim.Value);
            if (user == null)
            {
                return NotFound("user not found");
            }

            var todos = await _todoContext.TodoItems
            .Where(t => t.UserId == user)
            .ToListAsync();

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

            var todo = new TodoItem
            {
                Title = todoItem.Title,
                Description = todoItem.Description,
                IsCompleted = false,
                UserId = userId
            };

            _todoContext.TodoItems.Add(todo);
            await _todoContext.SaveChangesAsync();

            return Ok(todo);
        }

        // update entire todo
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateFullTask(int id, [FromBody] TodoItem todoItem)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            int userId = int.Parse(userIdClaim.Value);

            var todo = await _todoContext.TodoItems.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (todo == null)
            {
                return NotFound("Todo not found.");
            }

            todo.Title = todoItem.Title;
            todo.Description = todoItem.Description;
            todo.IsCompleted = todoItem.IsCompleted;

            await _todoContext.SaveChangesAsync();
            return Ok(todo);
        }

        // update isCompleted 
        [HttpPut("completed/{id}")]
        public async Task<IActionResult> UpdateCompletedTask(int id, [FromBody] TodoItem todoItem)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            int userId = int.Parse(userIdClaim.Value);

            var todo = await _todoContext.TodoItems.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (todo == null)
            {
                return NotFound("Todo not found.");
            }

            todo.IsCompleted = todoItem.IsCompleted;

            await _todoContext.SaveChangesAsync();
            return Ok(todo);
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            int userId = int.Parse(userIdClaim.Value);

            var todo = await _todoContext.TodoItems.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (todo == null)
            {
                return NotFound("Todo not found.");
            }

            _todoContext.Remove(todo);
            await _todoContext.SaveChangesAsync();
            return Ok("Todo deleted.");
        }

        // admin should have this role
        [HttpDelete("user/delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            int userId = int.Parse(userIdClaim.Value);

            var user = await _todoContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            _todoContext.Remove(user);
            await _todoContext.SaveChangesAsync();
            return Ok("User successfully deleted.");
        }
    }
}
