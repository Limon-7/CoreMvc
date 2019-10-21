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
    [Route("[Controller]/[Action]")]
    public class HomeController : Controller
    {
        private readonly IStudentRepository _IStudentRepository;

        public HomeController(IStudentRepository IStudentRepository){
            _IStudentRepository=IStudentRepository;
        }
       // [Route("")]
        //[Route("Home")]
        // [Route("Home/Index")]
        //Action 
        [Route("~/Home")]
        [Route("~/")]
        [Route("")]
        public ViewResult Index(){
            var student=_IStudentRepository.GetAllStudent();
            return View(student);
        }
        //? use id parameter to make optional and make parameter nullable
        //id?? 1 use to set default id=1
       // [Route("Home/Details/{id?}")]
        [Route("{id?}")]

        public ViewResult Details(int? id){
            StudentDetailsViewModel studentViewModelDetails= new StudentDetailsViewModel(){
                Student=_IStudentRepository.GetStudent(id??1),
                Title="Student Details"
            };
             return View(studentViewModelDetails);
        }

        [Route("")]
        [HttpGet]
        public ViewResult Create(){
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student stu){
            if(ModelState.IsValid){
                
           Student newStudent=_IStudentRepository.Add(stu);
           return RedirectToAction("Details",new {newStudent.Id});
        }
        return View();
        }

    }
}
