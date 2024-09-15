using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyERP.Core.Models
{
    public class Payment : BaseEntity
    {
        // The payment belongs to one customer.

        public int CustomerId { get; set; }
        public double Amount { get; set; }
        public Customer Customer { get; set; }
    }
}
