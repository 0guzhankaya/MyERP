using MyERP.Core.Models;
using MyERP.Core.Repositories;
using MyERP.Core.Services;
using MyERP.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyERP.Service.Services
{
    // Primary Constructor came in .NET8
    public class CustomerService(IGenericRepository<Customer> repository, IUnitOfWorks unitOfWorks, ICustomerRepository customerRepository) : GenericService<Customer>(repository, unitOfWorks), ICustomerService
    {
        private readonly ICustomerRepository customerRepository = customerRepository; // Dependency Injection
    }
}
