using System;
using System.IO;
using Microsoft.AspNetCore.Authorization;
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

        public HomeController(IStudentRepository IStudentRepository, IWebHostEnvironment iWebHosting)
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
        [AllowAnonymous]
        public ViewResult Index()
        {
            var student = _IStudentRepository.GetAllStudent();
            return View(student);
        }
        //? use id parameter to make optional and make parameter nullable
        //id?? 1 use to set default id=1
        // [Route("Home/Details/{id?}")]
        [Route("{id?}")]

        [AllowAnonymous]
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
                string uniquePhoto = ProcessUploadFile(stu);
                Student newStudent = new Student
                {
                    Name = stu.Name,
                    Email = stu.Email,
                    Department = stu.Department,
                    PhotoPath = uniquePhoto
                };
                _IStudentRepository.Add(newStudent);
                return RedirectToAction("Details", new { newStudent.Id });
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Student student = _IStudentRepository.GetStudent(id);
            StudentEditViewModel studentEditViewModel = new StudentEditViewModel
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Department = student.Department,
                ExistingPhotoPath = student.PhotoPath
            };
            return View(studentEditViewModel);
        }
        [HttpPost]
        public IActionResult Edit(StudentEditViewModel editViewModel)
        {
            /* 
            if (ModelState.IsValid)
            {
                // Retrieve the employee being edited from the database
                Student student = _IStudentRepository.GetStudent(editViewModel.Id);
                // Update the employee object with the data in the model object
                student.Name = editViewModel.Name;
                student.Email = editViewModel.Email;
                student.Department = editViewModel.Department;

                // If the user wants to change the photo, a new photo will be
                // uploaded and the Photo property on the model object receives
                // the uploaded photo. If the Photo property is null, user did
                // not upload a new photo and keeps his existing photo
                if (editViewModel.Photo != null)
                {
                    // If a new photo is uploaded, the existing photo must be
                    // deleted. So check if there is an existing photo and delete
                    if (editViewModel.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(_iWebHosting.WebRootPath,
                            "images", editViewModel.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    // Save the new photo in wwwroot/images folder and update
                    // PhotoPath property of the employee object which will be
                    // eventually saved in the database
                    student.PhotoPath = ProcessUploadFile(editViewModel);
                }

                // Call update method on the repository service passing it the
                // employee object to update the data in the database table
                Student upDateStudent = _IStudentRepository.Update(student);

                return RedirectToAction("index");
            }

            return View(editViewModel);
        }*/

            if(ModelState.IsValid){
             Student student = _IStudentRepository.GetStudent(editViewModel.Id);
             student.Name=editViewModel.Name;
             student.Email=editViewModel.Email;
             student.Department=editViewModel.Department;
             if(editViewModel.Photo!=null){
                 if(editViewModel.ExistingPhotoPath!=null){
                     string filePath = Path.Combine(_iWebHosting.WebRootPath, "images", editViewModel.ExistingPhotoPath);
                     System.IO.File.Delete(filePath);
                 }
                  student.PhotoPath = ProcessUploadFile(editViewModel);
             }
             Student upDateStudent = _IStudentRepository.Update(student);
             return RedirectToAction("Index");
         }

             return View(editViewModel);
         }
        private string ProcessUploadFile(StudentCreateViewModel stu)
        {
            string uniquePhoto = null;
            /* Multiple file upload And In modelcreate view Class List<IFormFile> must be generic  
            if(stu.Photo!=null && stu.Photo.Count>0){
                 foreach (IFormFile photo in stu.Photo)
                {
                    string uploadsFolder = Path.Combine(_iWebHosting.WebRootPath, "images");
                    uniquePhoto = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniquePhoto);
                    photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

            }*/

            //For single file upload
            if (stu.Photo != null)
            {
                string uploadsFolder = Path.Combine(_iWebHosting.WebRootPath, "images");
                uniquePhoto = Guid.NewGuid().ToString() + "_" + stu.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniquePhoto);
                stu.Photo.CopyTo(new FileStream(filePath, FileMode.Create));

            }
            return uniquePhoto;
        }
    }
}
