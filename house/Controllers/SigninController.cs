﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using house.ViewModels.Signin;
using house.ActionModels.Signin;
using house.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Linq;

namespace house.Controllers
{
    public class SigninController : Controller
    {

        public IActionResult New()
        {
            if(User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            string returnUrlParamName = CookieAuthenticationDefaults.ReturnUrlParameter;

            string returnUrl = Request.Query[returnUrlParamName].FirstOrDefault();

            return View(new NewViewModel(returnUrl));
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( CreateActionModel data
                                   , [FromServices] SignInManager<AppUser> _signInManager)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            string returnUrlParamName = CookieAuthenticationDefaults.ReturnUrlParameter;

            string returnUrl = Request.Query[returnUrlParamName].FirstOrDefault();


            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "There were validation errors");

                return View("New", data.MapToNewViewModel(returnUrl));
            }

  
            var result = await _signInManager.PasswordSignInAsync( data.SigninForm?.Email
                                                           , data.SigninForm?.Password
                                                           , isPersistent: true
                                                           , lockoutOnFailure: true);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Invalid login");

                return View("New", data.MapToNewViewModel(returnUrl));
            }


            //return LocalRedirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

      
        public async Task<IActionResult> Remove([FromServices] SignInManager<AppUser> _signInManager)
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

    }
}
