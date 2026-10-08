using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Options;
using TaskManager_API.Data;

var builder = WebApplication.CreateBuilder(args);

// Enable Controllers
builder.Services.AddControllers();

// Enable PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

var app = builder.Build();

app.MapControllers();

/* Get

app.MapGet("/", () => "Hello World!");

app.MapGet("/tasks", () =>
{
    return tasks;
    
});

app.MapGet("/tasks/{id}", (int id) =>
{
    var task = tasks.FirstOrDefault(t => t.Id == id);

    if (task == null)
    {
        return Results.NotFound("Task Not Found.");
    }

    return Results.Ok(task);
});

*/

/* Post 

app.MapPost("/tasks", (TaskRequest task) =>
{
    return new
    {
        Id = 1,
        Title = task.Title,
        Completed = false 
    };
});

*/

// app.UseHttpsRedirection();

app.Run(); 

// record TaskRequest(string Title); 