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
    
    public class TicketController : ControllerBase
    {
        private readonly ITickets ticketRepo;

        public TicketController(ITickets ticketRepo)
        {
            this.ticketRepo = ticketRepo;
        }



        [HttpGet]
        public async Task<ActionResult> GetTicket()
        {
            try
            {
                return Ok(await ticketRepo.GetTickets());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }


        [HttpGet("{UID}")]
        public async Task<ActionResult> GetTicketbyUID(string UID)
        {
            try
            {
                return Ok(await ticketRepo.GetTicketByUID(UID));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Ticket>> CreateTicket(Ticket Ticket)
        {
            try
            {
                if (Ticket == null)
                    return BadRequest();

                var createdTicked = await ticketRepo.AddTicket(Ticket);

                return CreatedAtAction(nameof(CreateTicket),
                    new { id = createdTicked.IDTicket }, createdTicked);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new Ticket record");
            }
        }

        [HttpPut("{ticketID:int}")]
        public async Task<ActionResult<Ticket>> UpdateTicket(int ticketID, Ticket Ticket)
        {
            try
            {
                if (ticketID != Ticket.IDTicket)
                    return BadRequest("Ticket ID mismatch");

                var ticketToUpdate = await ticketRepo.GetTicket(ticketID);

                if (ticketToUpdate == null)
                {
                    return NotFound($"Ticket with UID = {ticketID} not found");
                }

                return await ticketRepo.UpdateTicket(Ticket);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating Ticket record");
            }
        }

        [HttpDelete("{ticketID:int}")]
        public async Task<ActionResult> DeleteTicket(int ticketID)
        {
            try
            {
                var ticketToDelete = await ticketRepo.GetTicket(ticketID);

                if (ticketToDelete == null)
                {
                    return NotFound($"Ticket with UID = {ticketID} not found");
                }

                await ticketRepo.DeleteTicket(ticketID);

                return Ok($"Ticket with UID = {ticketID} deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting employee record");
            }
        }
    }
}
