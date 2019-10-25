namespace StudentMvc.ViewModels
{
    public class StudentEditViewModel:StudentCreateViewModel
    {
        public int Id{get;set;}
        public string ExistingPhotopath{get;set;}

    }
}