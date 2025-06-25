using ETaskRep.Models;
using Microsoft.EntityFrameworkCore;
using ETaskRep.Models; 

namespace ETaskRep.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } 
        public DbSet<Registration> Register { get; set; }
    }
}