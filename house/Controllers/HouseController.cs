using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using house.Data;
using house.ViewModels.House;
using house.ActionModels.House;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace house.Controllers
{
    public class HouseController : Controller
    {
        private readonly ILogger<HouseController> _logger;

        public HouseController(ILogger<HouseController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index([FromServices]HouseRepository repo)
        {
            return View(new IndexViewModel(repo.Houses()));
        }


        public IActionResult Show(int id
                                  ,[FromServices]HouseRepository repo)
        {
            var house = repo.House(id);

            if (house == null)
                return View("NotFound");

            return View(new ShowViewModel(house));
        }

        [Authorize]
        public IActionResult New()
        {
            return View(new NewViewModel());
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateActionModel data
                                    , [FromServices]HouseRepository repo)
        {           
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("","There were validation errors");

                return View("New", data.MapToNewViewModel());
            }

            var house = data.House.MapToHouse();

            repo.Add(house);

            return RedirectToAction("Show", new { house.Id });
        }

        [Authorize]
        public IActionResult Edit(int id
                                  , [FromServices]HouseRepository repo)
        {
            var house = repo.House(id);

            if (house == null)
                return RedirectToAction("Error", "Home");
         

            return View(new EditViewModel(house));

        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Update(UpdateActionModel data
                                    , [FromServices]HouseRepository repo)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "There were validation errors");
                return View("Edit", data.MapToEditViewModel());
            }

            var trackedhouse = repo.House(data.House.Id);

            if (trackedhouse == null)
                return RedirectToAction("Error", "Home");

            repo.Detach(trackedhouse);

            var house = data.House.MapToHouse();

            repo.Update(house);

            return RedirectToAction("Show", new { house.Id });
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            return View(id);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Destroy(int id
                                     , [FromServices]HouseRepository repo)
        {
            var trackedhouse = repo.House(id);

            if (trackedhouse == null)
                return RedirectToAction("Error", "Home");

            repo.Detach(trackedhouse);

            repo.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
