using MyERP.Core.Models;
using MyERP.Core.Repositories;
using MyERP.Core.Services;
using MyERP.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyERP.Service.Services
{
    public class GroupInRoleService : GenericService<GroupInRole>, IGroupInRoleService
    {
        public GroupInRoleService(IGenericRepository<GroupInRole> repository, IUnitOfWorks unitOfWorks) : base(repository, unitOfWorks)
        {
        }
    }
}
