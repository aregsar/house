using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using house.ViewModels.Signup;
using house.ActionModels.Signup;

namespace house.Controllers
{
    public class SignupController : Controller
    {
 
        public IActionResult New()
        {
            return View(new NewViewModel());
        }

        public IActionResult Create(CreateActionModel data)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "There were validation errors");

                return View("New", data.MapToNewViewModel());
            }

            TempData.Add("Flash", "Sign up success");

            return RedirectToAction("Index", "Home");
        }
    }
}
