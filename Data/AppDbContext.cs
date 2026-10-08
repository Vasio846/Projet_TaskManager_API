using Microsoft.EntityFrameworkCore;
using TaskManager_API.Models;

namespace TaskManager_API.Data;

// Contient le lien entre l'application et PostgreSQL 
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {  
    }
    
    public DbSet<TaskItem> Tasks { get; set; }
}
