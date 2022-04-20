using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EvenQ_API.Model
{
    public class Member
    { 
        
        [Key]
        public string UID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RefferalCode { get; set; }
        public bool IsAdmin { get; set; }
        public int NumberOfRefferals { get; set; }
        public bool MembershipValid { get; set; }
    }
}
