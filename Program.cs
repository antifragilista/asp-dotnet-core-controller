// Program.cs
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Registering the TodoDb context in the DI (Dependency Injection) container allows the app to now receive and use TodoDb instances throughout.
// This eliminates the need to create TodoDb objects in other parts of the application.
// This registration is done through the builder.Services object.
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
// This method registers MVC controller services in the DI (Dependency Injection) container, enabling controllers to automatically receive the dependencies they require.
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();