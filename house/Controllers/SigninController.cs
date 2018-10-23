using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using house.ViewModels.Signin;
using house.ActionModels.Signin;
using house.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace house.Controllers
{
    public class SigninController : Controller
    {
        private readonly ILogger<SigninController> _logger;

        public SigninController(ILogger<SigninController> logger)
        {
            _logger = logger;
        }

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
                                   , [FromServices] SignInManager<AppUser> signInManager)
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

            var result = await signInManager.PasswordSignInAsync(data.SigninForm?.Email
                                                                , data.SigninForm?.Password
                                                                , isPersistent: true
                                                                , lockoutOnFailure: true);

            if (!result.Succeeded)
            {
                if(result.IsLockedOut)
                    ModelState.AddModelError(string.Empty, "Locked out. Wait one minute before trying again");
                else
                    ModelState.AddModelError(string.Empty, "Invalid login");

                return View("New", data.MapToNewViewModel(returnUrl));
            }

            if (!Url.IsLocalUrl(returnUrl))
                return RedirectToAction("Index", "Home");

            return LocalRedirect(returnUrl);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove([FromServices] SignInManager<AppUser> signInManager)
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

    }
}
