using System.Collections.Generic;
using System.Linq;

namespace StudentMvc.Models
{
    public class MockStudentRepository : IStudentRepository
    {
        private List<Student> _studentList;
        public MockStudentRepository(){
            _studentList=new List<Student>{
                new Student(){Id=1,Name="Limon",Email="liibd7@gmail.com",Department=Dept.Cse},
                new Student(){Id=2,Name="Sam",Email="Sambd7@gmail.com",Department=Dept.Mec},
                new Student(){Id=3,Name="Oyon",Email="Oionbd7@gmail.com",Department=Dept.Cse},
                new Student(){Id=4,Name="Nion",Email="nionbd7@gmail.com",Department=Dept.None},
            };
        }

        public Student Add(Student student)
        {
            student.Id=_studentList.Max(i=>i.Id)+1;
            _studentList.Add(student);
            return student;
        }

        public Student Delete(int id)
        {
            Student student=_studentList.FirstOrDefault(d=>d.Id==id);
            if(student!=null){
                _studentList.Remove(student);
            }
            return student;
        }

        public IEnumerable<Student> GetAllStudent()
        {
            return _studentList;
        }

        public Student GetStudent(int id)
        {
            return _studentList.FirstOrDefault(e=>e.Id==id);
        }

        public Student Update(Student updatechanges)
        {
            Student student=_studentList.FirstOrDefault(u=>u.Id==updatechanges.Id);
            if(student!=null){
                student.Name=updatechanges.Name;
                student.Email=updatechanges.Email;
                student.Department=updatechanges.Department;
            }
            return student;
        }
    }
}