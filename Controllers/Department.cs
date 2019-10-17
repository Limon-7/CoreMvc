using Microsoft.AspNetCore.Mvc;

namespace StudentMvc.Controllers
{
    public class Department: Controller
    {
        public string List(){
            return "List () of DepartmentController ";
        }
        public string Details(){
            return "Details () of DepartmentController ";
        }
        
    }
}