using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EvenQ_API.Model
{
    public class Refferal
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int IDRefferal { get; set; }
        public Member Inviter { get; set; }

        public string InviterId { get; set; }

        public Member Invitee { get; set; }

        public string InviteeId { get; set; }
        public DateTime Date { get; set; }
    }
}
