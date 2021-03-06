﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using house.Data;
using house.ResponseModels;
using house.ActionModels.HouseApi;
using Microsoft.Extensions.Logging;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace house.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class HouseApiController : ControllerBase
    {

        private readonly HouseRepository _houseRepository;
        private readonly ILogger<HouseApiController> _logger;
           
        public HouseApiController(HouseRepository houseRepository
                                  , ILogger<HouseApiController> logger)
        {
            _houseRepository = houseRepository;
            _logger = logger;
        }


        [HttpGet("")]
        public IActionResult Index()
        {
            return Ok( _houseRepository.Houses().Select(house => new HouseResponseModel(house)) );       
        }

        [HttpGet("{id:int}")]
        public IActionResult Show(int id)
        {
            var house = _houseRepository.House(id);

            if (house == null) 
                return NotFound();

            return Ok(new HouseResponseModel(house));
        }

        [HttpPost("")]
        public IActionResult Post(CreateActionModel postData)
        {
            House house = postData.MapToHouse();

            _houseRepository.Add(house);
                           
            return CreatedAtAction(nameof(Show),
                                new { id = house.Id }, new HouseResponseModel(house));

        }

        [HttpPut("")]
        public IActionResult Put(UpdateActionModel putData)
        {
            House attachedHouse = _houseRepository.House(putData.Id);
            
            if (attachedHouse == null)
                return NotFound();

            _houseRepository.Detach(attachedHouse);

            House house = putData.MapToHouse();

            _houseRepository.Update(house);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
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
