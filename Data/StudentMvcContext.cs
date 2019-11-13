using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentMvc.Data;

namespace StudentMvc.Models
{
    public class StudentMvcContext : IdentityDbContext {
        public StudentMvcContext (DbContextOptions<StudentMvcContext> options) : base (options) { }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }  
    }
}