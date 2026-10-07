using Microsoft.EntityFrameworkCore;
using TaskModel = TaskManager.Models.Task;

namespace TaskManager.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {  
    }
    
    public DbSet<TaskModel> Tasks { get; set; }
}
