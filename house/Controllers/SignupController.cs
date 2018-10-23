using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using house.ViewModels.Signup;
using house.ActionModels.Signup;
using house.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace house.Controllers
{
    public class SignupController : Controller
    {
        private readonly ILogger<SignupController> _logger;

        public SignupController(ILogger<SignupController> logger)
        {
            _logger = logger;
        }

        public IActionResult New()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View(new NewViewModel());
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( CreateActionModel data
                                               , [FromServices] UserManager<AppUser> userManager)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

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
