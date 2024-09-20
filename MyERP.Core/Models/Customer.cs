using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyERP.Core.Models
{
    public class Customer : BaseEntity
    {
        // One customer has many payments and one payment have one customer.
        // One customer has many sales and one sale have one customer.
        // One to Many Relationship

        public string Name { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<Sale> Sales { get; set; }
    }
}
