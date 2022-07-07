using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EvenQ_API.Attributes;
using EvenQ_API.Model;
using EvenQ_API.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvenQ_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class MemberController : ControllerBase
    {
        private readonly IMemberRepo memberRepo;

        public MemberController(IMemberRepo memberRepo)
        {
            this.memberRepo = memberRepo;
        }

       [HttpGet("MemberName/{name}")]
        public async Task<ActionResult<IEnumerable<Member>>> SearchMember(string name)
        {
            try
            {
                var result = await memberRepo.SearchMember(name);

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
        public async Task<ActionResult> GetMembers()
        {
            try
            {
                return Ok(await memberRepo.GetMembers());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("Admin/{UID}")]
        public async Task<ActionResult<Member>> IsMemberAdmin(string UID)
        {
            try
            {
                var result = await memberRepo.IsAdmin(UID);

                if (result == null)
                {
                    return NotFound("Data doesn't exist or member is not an admin.");
                }

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{UID}")]
        public async Task<ActionResult<Member>> GetMember(string UID)
        {
            try
            {
                var result = await memberRepo.GetMember(UID);

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
        public async Task<ActionResult<Member>> CreateMember(Member member)
        {
            try
            {
                if (member == null)
                    return BadRequest();

                var createdMember = await memberRepo.AddMember(member);

                return CreatedAtAction(nameof(CreateMember),
                    new { id = createdMember.UID }, createdMember);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new member record");
            }
        }

        [HttpPut("{UID}")]
        public async Task<ActionResult<Member>> UpdateMember(string UID, Member member)
        {
            try
            {
                if (UID != member.UID)
                    return BadRequest("Member ID mismatch");

                var memberToUpdate = await memberRepo.GetMember(UID);

                if (memberToUpdate == null)
                {
                    return NotFound($"Member with UID = {UID} not found");
                }

                return await memberRepo.UpdateMember(member);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating member record");
            }
        }

        [HttpPut("Admin/{UID}")]
        public async Task<ActionResult<Member>> UpdateMemberAdmin(string UID, Member member)
        {
            try
            {
                if (UID != member.UID)
                    return BadRequest("Member ID mismatch");

                var memberToUpdate = await memberRepo.GetMember(UID);

                if (memberToUpdate == null)
                {
                    return NotFound($"Member with UID = {UID} not found");
                }

                return await memberRepo.UpdateMemberAdmin(member);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating member record");
            }
        }

        [HttpDelete("{UID}")]
        public async Task<ActionResult> DeleteMember(string UID)
        {
            try
            {
                var memberToDelete = await memberRepo.GetMember(UID);

                if (memberToDelete == null)
                {
                    return NotFound($"Member with UID = {UID} not found");
                }

                await memberRepo.DeleteMember(UID);

                return Ok($"Member with UID = {UID} deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting employee record");
            }
        }



        [HttpGet("Check/{ReffralCode}")]
        public async Task<bool> IsInviteValid(string ReffralCode)
        {
            try
            {
                var result = await memberRepo.IsInviteValid(ReffralCode);
               
                return result;
            }
            catch (Exception)
            {
                return false;
            }
                
            
        }


    }
}
