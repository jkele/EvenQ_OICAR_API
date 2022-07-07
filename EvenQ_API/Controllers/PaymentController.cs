using EvenQ_API.Model;
using EvenQ_API.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenQ_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PaymentController : ControllerBase
    {
        private readonly IPayment paymentRepo;

        public PaymentController(IPayment paymentRepo)
        {
            this.paymentRepo = paymentRepo;
        }

        [HttpGet("{UID}")]
        public async Task<ActionResult<IEnumerable<Payment>>> SearchPayments(string name)
        {
            try
            {
                var result = await paymentRepo.SearchPayments(name);

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
        public async Task<ActionResult> GetPayments()
        {
            try
            {
                return Ok(await paymentRepo.GetPayments());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{paymentID:int}")]
        public async Task<ActionResult<Payment>> GetPayment(int paymentID)
        {
            try
            {
                var result = await paymentRepo.GetPayment(paymentID);

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
        public async Task<ActionResult<Payment>> CreatePayment(Payment payment)
        {
            try
            {
                if (payment == null)
                    return BadRequest();

                var createdMember = await paymentRepo.AddPayment(payment);

                return CreatedAtAction(nameof(CreatePayment),
                    new { id = createdMember.UID }, createdMember);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new payment record");
            }
        }


        [HttpPut("{paymentID:int}")]
        public async Task<ActionResult<Payment>> UpdatePayment(Payment payment)
        {
            try
            {
                var paymentToUpdate = await paymentRepo.GetPayment(payment.IDPayment);

                if (paymentToUpdate == null)
                {
                    return NotFound($"Payment with id = {payment.IDPayment} not found");
                }

                return await paymentRepo.UpdatePayment(payment);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating payment record");
            }
        }

        [HttpDelete("{paymentID:int}")]
        public async Task<ActionResult> DeletePayment(int id)
        {
            try
            {
                var paymentToDelete = await paymentRepo.GetPayment(id);

                if (paymentToDelete == null)
                {
                    return NotFound($"Payment with ID = {id} not found");
                }

                await paymentRepo.DeletePayment(id);

                return Ok($"Payment with ID = {id} deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting payment record");
            }
        }
    }
}
