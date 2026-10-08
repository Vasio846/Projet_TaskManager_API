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

    [HttpGet("{id}")] // représente : GET /tasks/{id}
    public async Task<ActionResult<TaskItem>> GetTask(int id)
    // ActionResult<TaskItem> : retourne une réponse HTTP contenant un objet Task
    {
        var taskItem = await _context.Tasks.FindAsync(id);
        // Find the TaskItem whose Id is id  

        if(taskItem == null)
        {
            return NotFound();
        }

        return Ok(taskItem);
    }

    [HttpPost] // Méthode exécutée quand une requête HTTP POST arrive 
    public async Task<ActionResult<TaskItem>> CreateTask([FromBody] TaskItem taskItem)
    // Task<..> : System.Threading.Tasks.Task (opération asynchrone) 
    // ActionResult<TaskItem> : retourne une réponse HTTP contenant un objet Task 
    // [FromBody] TaskItem : prends le json dans le body HTTP et le transforme en TaskItem 
    {
        taskItem.CreatedAt = DateTime.UtcNow; // Date et Heure de création de la tâche 
        taskItem.Status = false; // une nouvelle tâche n'est pas terminée 

        _context.Tasks.Add(taskItem);
        // ajoute l'objet taskItem dans la collection Tasks gérée par EF

        await _context.SaveChangesAsync();
        // EF va générer une requête SQL équivalente à : INSERT INTO "Tasks" 

        return CreatedAtAction(
            nameof(GetTask), 
            new { id = taskItem.Id }, 
            taskItem
        );
        // retourne une réponse HTTP avec la tâche créée 
    }

    [HttpPut("{id}")] // répond aux requêted HTTP PUT /tasks/{id}
    public async Task<IActionResult> UpdateTask(int id, TaskItem updatedTask)
    // updatedTask vient du json : {"title":"Task Title", "description":"...", ...}
    {
        var task = await _context.Tasks.FindAsync(id);
        // trouve dans Tasks l'objet dont la clé primaire vaut id 

        if (task == null)
        {
            return NotFound();
        }

        // Modification de l'Objet (TaskTiem)
        task.Title = updatedTask.Title;
        task.Description = updatedTask.Description;
        task.Status = updatedTask.Status;
        task.Priority = updatedTask.Priority;

        await _context.SaveChangesAsync();
        // envoie les modifications à PostgreSQL 

        return Ok(task);
    }

    [HttpDelete("{id}")] // répond aux requêted HTTP DELETE /tasks/{id}
    public async Task<IActionResult> DeleteTask(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        // trouve dans Tasks l'objet dont la clé primaire vaut id 

        if (task == null)
        {
            return NotFound();
        }

        _context.Tasks.Remove(task);
        // marque l'objet comme devant être supprimé

        await _context.SaveChangesAsync();
        // effectue la modification : DELETE FROM tasks WHERE Id = id

        return NoContent();
        // Indique que la fonction a été exécutée mais qu'il n'y a rien à retourner 
    }
}

