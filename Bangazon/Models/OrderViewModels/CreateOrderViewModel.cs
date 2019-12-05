﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models.OrderViewModels
{
    public class CreateOrderViewModel
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public int? PaymentTypeId { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
        public Product Product { get; set; }
    }
}
