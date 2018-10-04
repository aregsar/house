using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using house.Data;
using house.ResponseModels;
using house.ActionModels.HouseApi;
using Microsoft.Extensions.Logging;

namespace house.Controllers
{
    public class HouseApiController : Controller
    {

        private readonly HouseRepository _houseRepository;
        private readonly ILogger<HouseApiController> _logger;
           
        public HouseApiController(HouseRepository houseRepository
                                  , ILogger<HouseApiController> logger)
        {
            _houseRepository = houseRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return Ok( _houseRepository.Houses().Select(house => new HouseResponseModel(house)) );       
        }


        public IActionResult Show(int id)
        {
            var house = _houseRepository.House(id);

            if (house != null) 
                return NotFound();

            return Ok(new HouseResponseModel(house));
        }

       
        public IActionResult Post([FromBody]CreateActionModel postData)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            House house = postData.MapToHouse();

            _houseRepository.Add(house);

            return Created(Url.Action("Index")
                           , new HouseResponseModel(house));

        }
            

        public IActionResult Put([FromBody]UpdateActionModel putData)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            House attachedHouse = _houseRepository.House(putData.Id);
            if (attachedHouse == null)
                return NotFound();

            _houseRepository.Detach(attachedHouse);

            House house = putData.MapToHouse();

            _houseRepository.Update(house);

            return NoContent();
        }

     
        public IActionResult Delete(int id)
        {
            House attachedHouse = _houseRepository.House(id);
            if (attachedHouse == null)
                return NotFound();

            _houseRepository.Detach(attachedHouse);

            _houseRepository.Delete(id);

            return NoContent();
        }
    }
}
