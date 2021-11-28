using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KnowBetter_WebApp.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
