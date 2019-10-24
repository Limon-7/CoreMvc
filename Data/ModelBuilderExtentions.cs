using Microsoft.EntityFrameworkCore;
using StudentMvc.Models;

namespace StudentMvc.Data
{
    public static class ModelBuilderExtentions
    {
        public static void Seed(this ModelBuilder modelBuilder){
            modelBuilder.Entity<Student>().HasData(
                new Student{
                    Id=4,
                    Name="Pinic",
                    Email="abc@gmail.com",
                    Department=Dept.Mec,
                    PhotoPath=""
                }
            );
        } 
    }
}