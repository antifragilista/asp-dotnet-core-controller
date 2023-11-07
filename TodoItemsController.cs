// TodoItemsController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// [ApiController] 어노테이션은 이 클래스가 API 컨트롤러임을 나타냅니다. 이는 모델 상태 검증과 같은 기능을 자동으로 활성화합니다.
[ApiController]
// 컨트롤러 이름을 기반으로 라우트 자동 설정. [controller]는 컨트롤러의 이름으로 대체됩니다. 예를 들어 TodoItemsController는 "TodoItems"로 해석됩니다.
[Route("[controller]")]
// [Route("todoitems")]
public class TodoItemsController : ControllerBase
{
    private readonly TodoDb _context;

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