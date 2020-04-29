using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UserManagement.Models
{
    public class Emp
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Contact { get; set; }
        public string UserRole { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class LoginModel
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string pass { get; set; }
    }
}