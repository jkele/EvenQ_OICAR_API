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

    public class EventController : ControllerBase
    {
        private readonly IEvents evenRepo;
        public EventController(IEvents evenRepo)
        {
            this.evenRepo = evenRepo;
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<Event>>> SearchEventTitle(string name)
        {
            try
            {
                var result = await evenRepo.SearchEventTitle(name);

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
        public async Task<ActionResult> GetEvents()
        {
            try
            {
                return Ok(await evenRepo.GetEvents());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet]
        [Route("UpcomingEvents")]
        public async Task<ActionResult> GetNewEvents()
        {
            try
            {
                return Ok(await evenRepo.GetUpcomingEvents());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{eventID:int}")]
        public async Task<ActionResult<Event>> GetEvent(int eventID)
        {
            try
            {
                var result = await evenRepo.GetEvent(eventID);

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
        public async Task<ActionResult<Event>> CreateEvent(Event Event)
        {
            try
            {
                if (Event == null)
                    return BadRequest();

                var createdEvent = await evenRepo.AddEvent(Event);

                return CreatedAtAction(nameof(CreateEvent),
                    new { id = createdEvent.IDEvent }, createdEvent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new Event record");
            }
        }

        [HttpPut("{eventID:int}")]
        public async Task<ActionResult<Event>> UpdateEvent(int eventID, Event Event)
        {
            try
            {
                if (eventID != Event.IDEvent)
                    return BadRequest("Event ID mismatch");

                var eventToUpdate = await evenRepo.GetEvent(eventID);

                if (eventToUpdate == null)
                {
                    return NotFound($"Event with UID = {eventID} not found");
                }

                return await evenRepo.UpdateEvent(Event);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating Event record");
            }
        }

        [HttpDelete("{eventID:int}")]
        public async Task<ActionResult> DeleteEvent(int eventID)
        {
            try
            {
                var eventToDelete = await evenRepo.GetEvent(eventID);

                if (eventToDelete == null)
                {
                    return NotFound($"Event with UID = {eventID} not found");
                }

                await evenRepo.DeleteEvent(eventID);

                return Ok($"Event with UID = {eventID} deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting employee record");
            }
        }
    }
}
