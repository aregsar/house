using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using house.ViewModels.Signin;
using house.ActionModels.Signin;
using house.Data;
using System.Threading.Tasks;


namespace house.Controllers
{
    public class SigninController : Controller
    {
        //private readonly SignInManager<AppUser> _signInManager;

        //public SigninController(SignInManager<AppUser> signInManager)
        //{
        //    _signInManager = signInManager;
        //}

        public IActionResult New()
        {
            return View(new NewViewModel());
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( CreateActionModel data
                                   , [FromServices] SignInManager<AppUser> _signInManager)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "There were validation errors");

                return View("New", data.MapToNewViewModel());
            }

            var result = await _signInManager.PasswordSignInAsync( data.SigninForm?.Email
                                                           , data.SigninForm?.Password
                                                           , isPersistent: true
                                                           , lockoutOnFailure: true);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Invalid login");

                return View("New", data.MapToNewViewModel());
            }

            return RedirectToAction("Index", "Home");
        }

      
        public async Task<IActionResult> Remove([FromServices] SignInManager<AppUser> _signInManager)
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

    }
}
