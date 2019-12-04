using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models.ProductViewModels
{
    public class MyProductsViewModel
    {
        public string Title { get; set; }
        [Display(Name = "Current Inventory")]
        public int Quantity { get; set; }
        public IEnumerable<Product> Products { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
