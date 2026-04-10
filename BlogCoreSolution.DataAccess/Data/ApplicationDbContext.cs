using BlogCore.Models;
using BlogCoreSolution.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogCoreSolution.DataAccess.Data;

public class ApplicationDbContext : IdentityDbContext 
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        :base(options)
    {
        
    }
    public DbSet<Category> Categories { get; set; }
}


