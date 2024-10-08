﻿using MyERP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyERP.Core.Services
{
    public interface ISaleService : IGenericService<Sale>
    {
        Task SaleProduct(Sale sale);
    }
}
