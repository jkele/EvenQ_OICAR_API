using EvenQ_API.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenQ_API.Repo
{
    public class PaymentRepo : IPayment
    {
        private readonly AppDbContext appDbContext;

        public PaymentRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Payment> AddPayment(Payment payment)
        {
            var result = await appDbContext.Payments.AddAsync(payment);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeletePayment(int paymentID)
        {
            var results = await appDbContext.Payments.FirstOrDefaultAsync(p => p.IDPayment == paymentID);

            if (results != null)
            {
                appDbContext.Payments.Remove(results);
                await appDbContext.SaveChangesAsync();
            }
        }

        public async Task<Payment> GetPayment(int paymentID)
        {
            return await appDbContext.Payments.FirstOrDefaultAsync(m => m.IDPayment == paymentID);
        }

        public async Task<IEnumerable<Payment>> GetPayments()
        {
            return await appDbContext.Payments.ToListAsync();
        }

        public async Task<IEnumerable<Payment>> SearchPayments(string uid)
        {
            return await appDbContext.Payments.Where(e => e.UID == uid).ToListAsync();
        }

        public async Task<Payment> UpdatePayment(Payment payment)
        {
            var results = await appDbContext.Payments.FirstOrDefaultAsync(m => m.IDPayment == payment.IDPayment);

            if (results != null)
            {
                results.DateBought = payment.DateBought;
                results.DateValid = payment.DateValid;
                results.IsMembership = payment.IsMembership;
                results.UID = payment.UID;



                await appDbContext.SaveChangesAsync();

                return results;
            }

            return null;
        }
    }
}
