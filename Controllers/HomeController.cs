using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentMvc.Models;
using StudentMvc.ViewModels;

namespace StudentMvc.Controllers
{
    public class HomeController : Controller
    {
        private IStudentRepository _IStudentRepository;

        public HomeController(IStudentRepository IStudentRepository){
            _IStudentRepository=IStudentRepository;
        }
        public string Index(){
            return _IStudentRepository.GetStudent(1).Name;
        }
        public ViewResult Details(){
            StudentDetailsViewModel studentViewModelDetails= new StudentDetailsViewModel(){
                Student=_IStudentRepository.GetStudent(3),
                Title="Student Details"
            };
             return View(studentViewModelDetails);
        }

    }
}
