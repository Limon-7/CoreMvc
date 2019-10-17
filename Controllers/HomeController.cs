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
        private readonly IStudentRepository _IStudentRepository;

        public HomeController(IStudentRepository IStudentRepository){
            _IStudentRepository=IStudentRepository;
        }
        public ViewResult Index(){
            var student=_IStudentRepository.GetAllStudent();
            return View(student);
        }
        public ViewResult Details(int id){
            StudentDetailsViewModel studentViewModelDetails= new StudentDetailsViewModel(){
                Student=_IStudentRepository.GetStudent(id),
                Title="Student Details"
            };
             return View(studentViewModelDetails);
        }

    }
}
