using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentMvc.Models;

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
        // public JsonResult Details(){
        //     Student model=_IStudentRepository.GetStudent(1);
        //     return Json(model);
        // }
        public ViewResult Details(){
            Student model= _IStudentRepository.GetStudent(1);
            ViewData["Student"]=model;
            ViewData["Title"]="Student Details";
            return View();
        }
    }
}
