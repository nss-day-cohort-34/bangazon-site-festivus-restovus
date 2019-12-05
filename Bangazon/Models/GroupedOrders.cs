using Bangazon.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bangazon.Models
{
    public class GroupedOrders
    {
        public int OrderId { get; set; }

        [Display(Name = "First Name")]
        public string UserFirstName { get; set; }

        [Display(Name = "Last Name")]
        public string UserLastName { get; set; }

        public ApplicationUser User { get; set; }

        [Display(Name = "Orders Placed")]
        public int OrderCount { get; set; }

        public IEnumerable<Order> Orders { get; set; }
    }
}