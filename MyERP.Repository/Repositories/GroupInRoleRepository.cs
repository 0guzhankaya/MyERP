﻿using MyERP.Core.Models;
using MyERP.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyERP.Repository.Repositories
{
    public class GroupInRoleRepository(AppDbContext context) : GenericRepository<GroupInRole>(context), IGroupInRoleRepository
    {
    }
}
