using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoreMVC.Models
{
    public class ApplicationDbContext :IdentityDbContext<AppUser> //do pridani user dedila jen  z class DbContext 
    {    public DbSet<Student> Students { get; set; }           //zustava stejne protoze class identity si to vsechno sama doda 
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
            
        }
    }
}
