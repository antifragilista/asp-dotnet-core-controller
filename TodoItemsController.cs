// TodoItemsController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// The [ApiController] annotation indicates that this class is an API controller.
// It automatically enables features such as model state validation.
[ApiController]
// Automatic route configuration based on controller name.
// The [controller] is replaced with the name of the controller.
// For example, TodoItemsController translates to "TodoItems".
[Route("[controller]")]
// [Route("todoitems")]
public class TodoItemsController : ControllerBase
{
    private readonly TodoDb _context;

    // The TodoDb type object is received as a parameter, which is automatically injected by the ASP.NET Core's Dependency Injection (DI) system.
    public TodoItemsController(TodoDb db)
    {
        _context = db;
    }

    [HttpGet]
    public async Task<ActionResult<List<Todo>>> GetAllTodos()
    {
        return await _context.Todos.ToListAsync();
    }

    [HttpGet("complete")]
    public async Task<ActionResult<List<Todo>>> GetCompleteTodos()
    {
        return await _context.Todos.Where(t => t.IsComplete).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Todo>> GetTodo(int id)
    {
        var todo = await _context.Todos.FindAsync(id);

        if (todo == null)
        {
            return NotFound();
        }

        return todo;
    }

    [HttpPost]
    public async Task<ActionResult<Todo>> CreateTodo(Todo todo)
    {
        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTodo), new { id = todo.Id }, todo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodo(int id, Todo inputTodo)
    {
        var todo = await _context.Todos.FindAsync(id);

        if (todo == null)
        {
            return NotFound();
        }

        todo.Name = inputTodo.Name;
        todo.IsComplete = inputTodo.IsComplete;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo(int id)
    {
        var todo = await _context.Todos.FindAsync(id);

        if (todo == null)
        {
            return NotFound();
        }

        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}