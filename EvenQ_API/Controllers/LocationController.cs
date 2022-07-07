using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EvenQ_API.Model;
using EvenQ_API.Repo;
using EvenQ_API.Attributes;

namespace EvenQ_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class LocationController : ControllerBase
    {
        private readonly ILocation locationRepo;

        public LocationController(ILocation locationRepo)
        {
            this.locationRepo = locationRepo;
        }

        [HttpGet("{searchLocation}")]
        public async Task<ActionResult<IEnumerable<Location>>> SearchLocation(string name)
        {
            try
            {
                var result = await locationRepo.SearchLocation(name);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }


        [HttpGet]
        public async Task<ActionResult> GetLocations()
        {
            try
            {
                return Ok(await locationRepo.GetLocations());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{locationID:int}")]
        public async Task<ActionResult<Location>> GetLocation(int locationID)
        {
            try
            {
                var result = await locationRepo.GetLocation(locationID);

                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Location>> CreateLocation(Location Location)
        {
            try
            {
                if (Location == null)
                    return BadRequest();

                var createdLocation = await locationRepo.AddLocation(Location);

                return CreatedAtAction(nameof(CreateLocation),
                    new { id = createdLocation.IDLocation }, createdLocation);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new Location record");
            }
        }

        [HttpPut("{locationID:int}")]
        public async Task<ActionResult<Location>> UpdateLocation(int locationID, Location Location)
        {
            try
            {
                if (locationID != Location.IDLocation)
                    return BadRequest("Location ID mismatch");

                var locationToUpdate = await locationRepo.GetLocation(locationID);

                if (locationToUpdate == null)
                {
                    return NotFound($"Location with UID = {locationID} not found");
                }

                return await locationRepo.UpdateLocation(Location);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating Location record");
            }
        }

        [HttpDelete("{locationID:int}")]
        public async Task<ActionResult> DeleteLocation(int locationID)
        {
            try
            {
                var locationToDelete = await locationRepo.GetLocation(locationID);

                if (locationToDelete == null)
                {
                    return NotFound($"Location with UID = {locationID} not found");
                }

                await locationRepo.DeleteLocation(locationID);

                return Ok($"Location with UID = {locationID} deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting employee record");
            }
        }
    }
}
