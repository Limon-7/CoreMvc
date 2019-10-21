using System.Collections.Generic;

namespace StudentMvc.Models
{
    public class SqliteStudentRepository : IStudentRepository
    {
        private readonly StudentMvcContext _context;
        public SqliteStudentRepository(StudentMvcContext _context)
        {
            this._context=_context;
        }
        public Student Add(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
            return student;
        }

        public Student Delete(int id)
        {
            Student student=_context.Students.Find(id);
            if(student!=null){
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
            return student;
        }

        public IEnumerable<Student> GetAllStudent()
        {
            return _context.Students;
        }

        public Student GetStudent(int id)
        {
            return _context.Students.Find(id);
        }

        public Student Update(Student updatechanges)
        {
            var student=_context.Students.Attach(updatechanges);
            student.State=Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return updatechanges;
        }
    }
}