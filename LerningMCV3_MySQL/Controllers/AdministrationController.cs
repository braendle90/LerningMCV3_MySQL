using LerningMCV3_MySQL.Data;
using LerningMCV3_MySQL.Models;
using LerningMCV3_MySQL.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LerningMCV3_MySQL.Controllers
{

    [Authorize]
    public class AdministrationController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserManager<ApplicationUser> userManager { get; }

        public AdministrationController(ApplicationDbContext context,RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel createRoleViewModel)
        {

            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = createRoleViewModel.RoleName
                };


                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "administration");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(createRoleViewModel);
        }

        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;

            return View(roles);

        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $" Role with ID = {id} cannot be found";

                return View("Not Found");
            }

            var model = new EditRoleViewModel
            {

                Id = role.Id,
                RoleName = role.Name
            };

            var users = await userManager.GetUsersInRoleAsync(role.Name);

            foreach (var user in users)
            {
                model.Users.Add(user.UserName);
            }

            return View(model);

        }


        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel viewModel)
        {
            var role = await roleManager.FindByIdAsync(viewModel.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $" Role with ID = {viewModel.Id} cannot be found";

                return View("Not Found");
            }
            else
            {
                role.Name = viewModel.RoleName;
                var result = await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(viewModel);
            }

        }




        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $" Role with ID = {roleId} cannot be found";

                return View("Not Found");
            }

            var model = new List<UserRoleViewModel>();

            foreach (var user in userManager.Users.ToList())
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    isSelected = await userManager.IsInRoleAsync(user, role.Name)
                };
                model.Add(userRoleViewModel);


                // var test = await userManager.IsInRoleAsync(user, "admin");

            }


            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {


            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with ID = {roleId} cannot be found";
                return View("Not Found");
            }
            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);
                IdentityResult identityResult = null;

                if (model[i].isSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    identityResult = await userManager.AddToRoleAsync(user, role.Name);
                    Debug.WriteLine("added");
                }
                else if (!model[i].isSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    identityResult = await userManager.RemoveFromRoleAsync(user, role.Name);
                    Debug.WriteLine("removed");
                }
                else
                {
                    continue;
                    //Because there are 2 more options what can be, if the user is selected and already in the role
                    //we do not what to do anything
                    //if the user is not selected and not in the role
                    //we do not what to do anything 
                    //so continue in code
                }
                if (identityResult.Succeeded)
                {
                    if (i < (model.Count) - 1)
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = roleId });
                }
            }

            return RedirectToAction("EditRole", new { Id = roleId });
        }



        [HttpGet]
        public async Task<IActionResult> DeleteRole(string id)
        {
            //var role = await roleManager.FindByIdAsync(id);

            var role = context.Roles.Where(r => r.Id == id).FirstOrDefault();
            context.Roles.Remove(role);
            context.SaveChanges();

            return RedirectToAction("ListRoles");

        }


    }
}
