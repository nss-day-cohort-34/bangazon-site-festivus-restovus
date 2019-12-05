using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models
{
    public class Account

    {
        [Required]
        public int AccountId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        public string StreetAddress { get; set; }

        public string PhoneNumber { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<PaymentType> PaymentTypes { get; set; }
    }
}
