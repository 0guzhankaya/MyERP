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
    public class DepartmentService : GenericService<Department>, IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentService(IGenericRepository<Department> repository, IUnitOfWorks unitOfWorks, IDepartmentRepository departmentRepository) : base(repository, unitOfWorks)
        {
            _departmentRepository = departmentRepository;
        }
    }
}
