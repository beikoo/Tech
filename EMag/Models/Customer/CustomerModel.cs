using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EMag.Models
{
    public class CustomerModel
    {
        public int ID { get; set; }
        [MaxLength(50)]
        [MinLength(1)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        [MinLength(1)]
        public string LastName { get; set; }
        [MaxLength(50)]
        [MinLength(6)]
        public string Address { get; set; }
        [MaxLength(24)]
        [MinLength(5)]
        [Required]
        public string Username { get; set; }
        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,16}$",ErrorMessage = "Password error")]
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}