using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [RouteAttribute("api/trips")]
    public class TripsController : Controller
    {
        private IWorldRepository _repository;
        private ILogger<TripsController> _logger;

        public TripsController(IWorldRepository repository, ILogger<TripsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        [HttpGetAttribute("")]
        // public JsonResult Get()   // Return Json can not return error 
        // {
        //     return Json(new Trip() { Name = "My Trip" });
        // }
        public IActionResult Get()
        {
            try
            {
                var result = _repository.GetAllTrips();
                return Ok(Mapper.Map<IEnumerable<TripVM>>(result));
            }
            catch (Exception exp)
            {
                _logger.LogError($"Failed to get all trips : {exp}");
                return BadRequest(exp);
            }
        }

        [HttpPostAttribute("")]
        public async Task<IActionResult> Post([FromBody]TripVM theTrip)
        {
            if (ModelState.IsValid)
            {
                // Save to database
                var newTrip = Mapper.Map<Trip>(theTrip);
                _repository.AddTrip(newTrip);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/trips/{theTrip.Name}", Mapper.Map<TripVM>(newTrip));
                }
            }
            return BadRequest("Failed to save changed to the database");
        }
    }
}