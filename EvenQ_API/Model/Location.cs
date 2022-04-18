using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EvenQ_API.Model
{
    public class Location
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int IDLocation { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Coordinates { get; set; }
    }
}
