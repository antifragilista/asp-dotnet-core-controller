// Program.cs
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// DI(Dependency Injection) 컨테이너에 TodoDb 컨텍스트를 등록
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
// Add controllers.
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();