using System.Collections.Generic;

namespace StudentMvc.Models
{
    public interface IStudentRepository
    {
        Student GetStudent(int Id);
        IEnumerable<Student>GetAllStudent();
    }
}