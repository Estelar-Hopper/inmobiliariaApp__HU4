using Microsoft.EntityFrameworkCore;
using inmobiliariaApp.Domain.Entities;

namespace inmobiliariaApp.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    { 
        
    }
    
    public DbSet<Property>  Properties { get; set; }
    public DbSet<User>  Users { get; set; }
    
}