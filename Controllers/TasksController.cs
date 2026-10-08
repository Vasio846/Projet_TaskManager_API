using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager_API.Data;

namespace TaskManager_API.Controllers;

[ApiController] // indique : Controller destiné à une API HTTP
[Route("tasks")] // début de l'URL ([HttpGet("{id}")] donnera : /tasks/id)
public class TasksController : ControllerBase // hérite de fonctionnalités HTTP
{
    private readonly AppDbContext _context;

    public TasksController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet] // Méthode exécutée quand uen requête HTTP GET arrive
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
}
