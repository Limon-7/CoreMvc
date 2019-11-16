using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentMvc.Models;
using StudentMvc.ViewModels;

namespace StudentMvc.Controllers
{
    [Authorize(Roles = "Admin,User")]

    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _user;

        public AdministrationController(RoleManager<IdentityRole> identityRole, UserManager<User> user)
        {
            _roleManager = identityRole;
            _user = user;
        }

        [HttpGet]
        public IActionResult ListUsers()
        {
            var user = _user.Users;
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> UserDelete(string id)
        {
            var user = await _user.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id={id} not found";
                return View("NotFound");
            }
            else
            {
                var result = await _user.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
                return View("ListUsers");
            }
        }
        [HttpGet]
        public async Task<IActionResult> UserEdit(string Id)
        {
            var user = await _user.FindByIdAsync(Id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id={Id} not found";
                return View("NotFound");
            }
            var userClaims = await _user.GetClaimsAsync(user);
            var userRoles = await _user.GetRolesAsync(user);
            var model = new UserEditViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Claims = userClaims.Select(c => c.Value).ToList(),
                Roles = userRoles
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UserEdit(UserEditViewModel model)
        {
            var user = await _user.FindByIdAsync(model.Id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id={model.Id} not found";
                return View("NotFound");
            }
            else
            {
                user.Email = model.Email;
                user.UserName = model.UserName;
                var result = await _user.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
                return View(model);
            }

        }

        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        [HttpPost]
        public async Task<IActionResult> RoleDelete(string id){
            var role=await _roleManager.FindByIdAsync(id);
            if(role==null){
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }
            else{
                var result= await _roleManager.DeleteAsync(role);
                if(result.Succeeded){
                    return RedirectToAction("ListRoles");
                }
                foreach(var err in result.Errors){
                    ModelState.AddModelError("",err.Description);
                }
                return RedirectToAction("ListRoles");
            }
            
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            // Find the role by Role ID
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");


            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            // Retrieve all the Users
            foreach (var user in _user.Users)
            {
                // If the user is in this role, add the username to
                // Users property of EditRoleViewModel. This model
                // object is then passed to the view for display
                if (await _user.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);
        }

        // This action responds to HttpPost and receives EditRoleViewModel
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;

                // Update the Role using UpdateAsync
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                IdentityResult result = await _roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Administration");
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id:{roleId} is not dound";
                return Redirect("NotFound");
            }
            var model = new List<UserRoleViewModel>();
            foreach (var user in _user.Users)
            {
                var userRoleViewModel = new UserRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (await _user.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id:{roleId} is not dound";
                return Redirect("NotFound");
            }
            for (int i = 0; i < model.Count; i++)
            {
                var user = await _user.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;
                if (model[i].IsSelected && !(await _user.IsInRoleAsync(user, role.Name)))
                {
                    result = await _user.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await _user.IsInRoleAsync(user, role.Name))
                {
                    result = await _user.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < (model.Count) - 1)
                    {
                        continue;
                    }
                    else
                    {
                        return RedirectToAction("EditRole", new { Id = roleId });
                    }
                }
            }
            return RedirectToAction("EditRole", new { Id = roleId });
        }

        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string id){
            ViewBag.userId=id;
            var user= await _user.FindByIdAsync(id);
            if(user ==null){
                ViewBag.ErrorMessage=$"User with Id={id} not found";
                return View("NotFound");
            }
            var model= new List<ManageUserRoleViewModel>();
            foreach(var role in _roleManager.Roles){
                var manageUserRole=new ManageUserRoleViewModel(){
                    RoleId=role.Id,
                    RoleName=role.Name
                };
                if(await _user.IsInRoleAsync(user,role.Name)){
                    manageUserRole.IsSelected=true;
                }
                else{
                    manageUserRole.IsSelected=false;
                }
                model.Add(manageUserRole);
            }
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> ManageUserRoles(List<ManageUserRoleViewModel> model, string id)
        {
            var user = await _user.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"Role with id:{id} is not dound";
                return Redirect("NotFound");
            }
            var role=await _user.GetRolesAsync(user);
            var result= await _user.RemoveFromRolesAsync(user,role);
            if(!result.Succeeded){
                ModelState.AddModelError("","Cannot remove user current role");
                return View(model);
            }
            result= await _user.AddToRolesAsync(user,model.Where(x=>x.IsSelected).Select(y=>y.RoleName));
            if(!result.Succeeded){
                ModelState.AddModelError("","Cannot remove user current role");
                return View(model); 
            }
            return RedirectToAction("UserEdit",new {Id=user.Id});
        }


    }
}