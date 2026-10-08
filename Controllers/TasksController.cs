using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager_API.Data;
using TaskManager_API.Models;

namespace TaskManager_API.Controllers;

[ApiController] // indique : Controller destiné à une API HTTP
[Route("tasks")] // début de l'URL ([HttpGet("{id}")] donnera : /tasks/id)
public class TasksController : ControllerBase // hérite de fonctionnalités HTTP
{
    private readonly AppDbContext _context;
    // Utilisé pour communiquer avec PostgreSQL

    public TasksController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet] // Méthode exécutée quand une requête HTTP GET arrive
    public async Task<IActionResult> GetTasks() 
    // async : opérations asynchrones
    // await : attends le résultat d'une opération asynchrone
    // IActionResult : indique que la méthode retourne une réponse HTTP  
    {
        var tasks = await _context.Tasks.ToListAsync(); 
        // _context.Tasks correspond à DbSet Tasks dans AppDbContext.cs 
        // .ToListAsync() récupère les lignes de Tasks et les transforme en liste C# 
        //    => SELECT * FROM Tasks;

        return Ok(tasks); // Réponse HTTP
    }

    [HttpPost] // Méthode exécutée quand une requête HTTP POST arrive 
    public async Task<ActionResult<TaskItem>> CreateTask([FromBody] TaskItem taskItem)
    // Task<..> : System.Threading.Tasks.Task (opération asynchrone) 
    // ActionResult<Task> : retourne une réponse HTTP contenant un objet Task 
    // [FromBody] TaskItem : prends le json dans le body HTTP et le transforme en TaskItem 
    {
        taskItem.CreatedAt = DateTime.UtcNow; // Date et Heure de création de la tâche 
        taskItem.Status = false; // une nouvelle tâche n'est pas terminée 

        _context.Tasks.Add(taskItem);
        // ajoute l'objet taskItem dans la collection Tasks gérée par EF

        await _context.SaveChangesAsync();
        // EF va générer une requête SQL équivalente à : INSERT INTO "Tasks" 

        // return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        // retourne une réponse HTTP avec la tâche créée 
        return Ok(taskItem);
    }
}
