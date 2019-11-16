using System.ComponentModel.DataAnnotations;

namespace StudentMvc.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name="Role")]
        public string RoleName{get;set;}
    }
}