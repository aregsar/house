using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using house.ViewModels.Signup;
using house.ActionModels.Signup;
using house.Data;
using Microsoft.AspNetCore.Identity;

namespace house.Controllers
{
    public class SignupController : Controller
    {

        public IActionResult New()
        {
            return View(new NewViewModel());
        }

        public async Task<IActionResult> Create( CreateActionModel data
                                   , [FromServices] UserManager<AppUser> userManager)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "There were validation errors");

                return View("New", data.MapToNewViewModel());
            }

            var user = new AppUser()
            {
                Email = data.SignupForm?.Email,                                                     
                UserName = data.SignupForm?.Email
            };

            var result = await userManager.CreateAsync( user
                                                      , data.SignupForm?.Password);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Failed to register user");

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View("New", data.MapToNewViewModel());
            }

            TempData.Add("Flash", "Sign up success");

            return RedirectToAction("Index", "Home");
        }
    }
}
