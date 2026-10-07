var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

var tasks = new[]
{
    new { Id = 1, Title = "Apprendre C#", Completed = false },
    new { Id = 2, Title = "Apprendre ASP.NET", Completed = false },
    new { Id = 3, Title = "Apprendre Docker", Completed = false}
};

// Get

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

// Post 

app.MapPost("/tasks", (TaskRequest task) =>
{
    return new
    {
        Id = 1,
        Title = task.Title,
        Completed = false 
    };
});

// app.UseHttpsRedirection();

app.Run(); 

record TaskRequest(string Title); 