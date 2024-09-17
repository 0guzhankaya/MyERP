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
    public class GroupService(IGenericRepository<Group> repository, IUnitOfWorks unitOfWorks) : GenericService<Group>(repository, unitOfWorks), IGroupService
    {
    }
}
