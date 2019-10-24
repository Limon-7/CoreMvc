using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using StudentMvc.Models;

namespace StudentMvc.ViewModels
{
    public class StudentCreateViewModel
    {
        [Required,MaxLength(50,ErrorMessage="Name can not be exceed 50 charecters"),MinLength(3,ErrorMessage="Name Must be at least 3 charecters")]
        public string Name{get;set;}
        [Required]
        [Display(Name="Student Email")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email format")]
        public string Email{get;set;}
        [Required(ErrorMessage="Please Select A value")]
        public Dept? Department{get;set;}
        public IFormFile Photo{get;set;}
    }
}