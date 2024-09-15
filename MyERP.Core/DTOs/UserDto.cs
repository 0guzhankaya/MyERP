using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyERP.Core.DTOs
{
    public class UserDto : BaseDto
    {
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public int GroupId { get; set; }
        public DepartmentDto Department { get; set; }
        public GroupDto Group { get; set; }
    }
}
