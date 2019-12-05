using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models.OrderViewModels
{
    public class UsersWithMultipleOrdersViewModel
    {
        public ApplicationUser User { get; set; }

        public List<ApplicationUser> Users { get; set; }

        public List<Order> Orders { get; set; }
    }
}
