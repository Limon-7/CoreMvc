using System.Collections.Generic;

namespace StudentMvc.Models
{
    public interface IStudentRepository
    {
        Student GetStudent(int id);
        IEnumerable<Student>GetAllStudent();
        Student Add(Student student);
        Student Delete(int id );
        Student Update(Student updatechanges);
    }
}