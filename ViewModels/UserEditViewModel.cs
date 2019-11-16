using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using StudentMvc.Models;

namespace StudentMvc.ViewModels
{
    public class UserEditViewModel:User
    {
        public UserEditViewModel()
        {
            Claims = new List<string>();
            Roles = new List<string>();
        }
        public List<string> Claims { get; set; }
        public IList<string> Roles { get; set; }
    }
}