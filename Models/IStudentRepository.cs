namespace StudentMvc.Models
{
    public interface IStudentRepository
    {
        Student GetStudent(int Id);
    }
}