using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using house.ViewModels.Signin;
using house.ActionModels.Signin;

namespace house.Controllers
{
    public class SigninController : Controller
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

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Remove()
        {
            return View("New");
        }
    }
}
