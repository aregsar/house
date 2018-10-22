using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace house.Controllers
{
    public class SignupController : Controller
    {
 
        public IActionResult New()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
