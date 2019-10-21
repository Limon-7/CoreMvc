using Microsoft.EntityFrameworkCore;

namespace StudentMvc.Models
{
    public class StudentMvcContext : DbContext {
        public StudentMvcContext (DbContextOptions<StudentMvcContext> options) : base (options) { }
        public DbSet<Student> Students { get; set; }
    }
}