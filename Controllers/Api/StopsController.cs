using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [RouteAttribute("/api/trips/{tripName}/stops")]
    public class StopsController : Controller
    {
        private IWorldRepository _repository;
        private ILogger<TripsController> _logger;
        private GeoCoordsService _coordsService;

        public StopsController(IWorldRepository repository,
                                ILogger<TripsController> logger,
                                GeoCoordsService coordsService)
        {
            _repository = repository;
            _logger = logger;
            _coordsService = coordsService;
        }

        [HttpGetAttribute("")]
        public IActionResult Get(string tripName)
        {
            try
            {
                var trip = _repository.GetTripByName(tripName);
                return Ok(Mapper.Map<IEnumerable<StopVM>>(trip.Stops.OrderBy(s => s.Order).ToList()));
            }
            catch (Exception exp)
            {
                _logger.LogError($"Failed to get all stop : {exp}");
                return BadRequest(exp);
            }
        }

        [HttpPostAttribute("")]
        public async Task<IActionResult> Post(string tripName, [FromBody]StopVM theStop)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Save to database
                    var newStop = Mapper.Map<Stop>(theStop);
                    var result = await _coordsService.GetCoordsAsync(newStop.Name);
                    if (!result.Success)
                    {
                        _logger.LogError(result.Message);
                    }
                    else
                    {
                        newStop.Latitude = result.Latitude;
                        newStop.Longitude = result.Longitude;

                        _repository.AddStop(tripName, newStop);

                        if (await _repository.SaveChangesAsync())
                        {
                            return Created($"api/trips/{tripName}/stops/{newStop.Name}", Mapper.Map<StopVM>(newStop));
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                _logger.LogError($"Failed to save new stop : {exp}");
            }
            return BadRequest("Failed to save save new stop");
        }
    }
}