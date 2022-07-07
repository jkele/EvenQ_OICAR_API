using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EvenQ_API.Model
{
    public class Payment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int IDPayment { get; set; }

        public DateTime DateBought { get; set; }
        public DateTime DateValid { get; set; }

        public bool IsMembership { get; set; }
        public string UID { get; set; }

    }
}
