using Microsoft.EntityFrameworkCore;
using TaskModel = TaskManager_API.Models.Task;
// Evite la confusion avec System.Threading.Tasks.Task de .NET

namespace TaskManager_API.Data;

// Contient le lien entre l'application et PostgreSQL 
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {  
    }
    
    public DbSet<TaskModel> Tasks { get; set; }
}
