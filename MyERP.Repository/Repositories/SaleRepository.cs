using MyERP.Core.Models;
using MyERP.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyERP.Repository.Repositories
{
    public class SaleRepository(AppDbContext context) : GenericRepository<Sale>(context), ISaleRepository
    {
    }
}
