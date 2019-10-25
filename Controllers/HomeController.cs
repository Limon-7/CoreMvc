using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentMvc.Models;
using StudentMvc.ViewModels;

namespace StudentMvc.Controllers
{
    [Route("[Controller]/[Action]")]
    public class HomeController : Controller
    {
        private readonly IStudentRepository _IStudentRepository;
        private readonly IWebHostEnvironment _iWebHosting;

        public HomeController(IStudentRepository IStudentRepository,  IWebHostEnvironment iWebHosting)
        {
            _IStudentRepository = IStudentRepository;
            this._iWebHosting = iWebHosting;
        }
        // [Route("")]
        //[Route("Home")]
        // [Route("Home/Index")]
        //Action 
        [Route("~/Home")]
        [Route("~/")]
        [Route("")]
        public ViewResult Index()
        {
            var student = _IStudentRepository.GetAllStudent();
            return View(student);
        }
        //? use id parameter to make optional and make parameter nullable
        //id?? 1 use to set default id=1
        // [Route("Home/Details/{id?}")]
        [Route("{id?}")]

        public ViewResult Details(int? id)
        {
            StudentDetailsViewModel studentViewModelDetails = new StudentDetailsViewModel()
            {
                Student = _IStudentRepository.GetStudent(id ?? 1),
                Title = "Student Details"
            };
            return View(studentViewModelDetails);
        }

        [Route("")]
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(StudentCreateViewModel stu)
        {
            if (ModelState.IsValid)
            {
                string uniquePhoto = null;
                if(stu.Photo!=null && stu.Photo.Count>0){
                     foreach (IFormFile photo in stu.Photo)
                    {
                        string uploadsFolder = Path.Combine(_iWebHosting.WebRootPath, "images");
                        uniquePhoto = Guid.NewGuid().ToString() + "_" + photo.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniquePhoto);
                        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    }
 
                }

                //For single file upload
                // if (stu.Photo != null)
                // {
                //     string uploadsFolder = Path.Combine(_iWebHosting.WebRootPath, "images");
                //     uniquePhoto = Guid.NewGuid().ToString() + "_" + stu.Photo.FileName;
                //     string filePath = Path.Combine(uploadsFolder, uniquePhoto);
                //     stu.Photo.CopyTo(new FileStream(filePath, FileMode.Create));

                // }
                Student newStudent = new Student{
                    Name=stu.Name,
                    Email=stu.Email,
                    Department=stu.Department,
                    PhotoPath=uniquePhoto
                };
                _IStudentRepository.Add(newStudent);
                return RedirectToAction("Details", new { newStudent.Id });
            }
            return View();
        }
    
        [HttpGet]
        public IActionResult Edit(int id){
            Student student=_IStudentRepository.GetStudent(id);
            StudentEditViewModel studentEditViewModel=new StudentEditViewModel{
                Id=student.Id,
                Name=student.Name,
                Email=student.Email,
                Department=student.Department,
                ExistingPhotopath=student.PhotoPath
            };
            return View(studentEditViewModel);
        }
        [HttpPost]
        public IActionResult Edit(){

            return View();
        }
    }
}
