using EvenQ_API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenQ_API.Repo
{
    public interface IPayment
    {
        Task<IEnumerable<Payment>> SearchPayments(string uid);
        Task<IEnumerable<Payment>> GetPayments();
        Task<Payment> GetPayment(int paymentID);
        Task<Payment> AddPayment(Payment payment);
        Task<Payment> UpdatePayment(Payment payment);
        Task DeletePayment(int paymentID);
    }
}
