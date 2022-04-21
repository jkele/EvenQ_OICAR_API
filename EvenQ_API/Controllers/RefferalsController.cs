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
    [ApiKey]
    public class RefferalsController : ControllerBase
    {
        private readonly IRefferals refferalsRepo;

        public RefferalsController(IRefferals refferalsRepo)
        {
            this.refferalsRepo = refferalsRepo;
        }


        [HttpGet("{refferalID:int}")]
        public async Task<ActionResult<Refferals>> GetRefferal(int refferalID)
        {
            try
            {
                var result = await refferalsRepo.GetRefferal(refferalID);

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
        public async Task<ActionResult<Refferals>> CreateRefferal(Refferals Refferals)
        {
            try
            {
                if (Refferals == null)
                    return BadRequest();
                var createdRefferal = await refferalsRepo.AddRefferal(Refferals);

                return CreatedAtAction(nameof(CreateRefferal),
                    new { id = createdRefferal.IDRefferal }, createdRefferal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new Refferals record");
            }
        }

        [HttpPut("{refferalID:int}")]
        public async Task<ActionResult<Refferals>> UpdateRefferal(int refferalID, Refferals Refferals)
        {
            try
            {
                if (refferalID != Refferals.IDRefferal)
                    return BadRequest("Refferals ID mismatch");

                var refferalToUpdate = await refferalsRepo.GetRefferal(refferalID);

                if (refferalToUpdate == null)
                {
                    return NotFound($"Refferals with UID = {refferalID} not found");
                }

                return await refferalsRepo.UpdateRefferal(Refferals);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating Refferals record");
            }
        }

    }
}
