using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

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

        // Line 22-27, DI implementation
        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        // GET: /api/TodoItems
        // returns all the items in TodoItems as a list. 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            return await _context.TodoItems.Select(x => ItemToDTO(x)).ToListAsync();
        }

        // GET: /api/TodoItems/5
        // returns a single item from TodoItems based on an id. 
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            // if there aren't any items, return NotFound()
            if (todoItem == null)
            {
                return NotFound();
            }

            // return the item if found
            return ItemToDTO(todoItem);
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItemDTO todoDTO)
        {
            // if the id doesn't equal to the id in TodoItem
            // return BadRequest()
            if (id != todoDTO.Id)
            {
                return BadRequest();
            }

            // _context.Entry(todoDTO).State = EntityState.Modified;

            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.Name = todoDTO.Name;
            todoItem.IsComplete = todoDTO.IsComplete;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*
            CREATE
            This method gets the value of the TodoItem from the body of the HTTP request.
            The CreatedAtAction method: 
                - returns an HTTP 201 status code. Standard response for an HTTP POST method that creates a new resource on the server
                - Adds a location header to the response. 
                - References the GetItemTodo action to create the Location header's URI. The nameof keyword is used to avoid hard-coding
                the action name in the CreatedAtAction call. 
        */
        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> PostTodoItem(TodoItemDTO todoDTO)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoDTO.IsComplete,
                Name = todoDTO.Name
            };

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, ItemToDTO(todoItem));
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }

        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
    }
}
