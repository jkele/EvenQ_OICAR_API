using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EvenQ_API.Model
{
    public class Ticket
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int IDTicket { get; set; }
        public string TicketQR { get; set; }
        public Member Member { get; set; }
        public string MemberId { get; set; }
        public Event Event { get; set; }
        public int EventId { get; set; }
        public bool IsValid { get; set; }
    }
}
