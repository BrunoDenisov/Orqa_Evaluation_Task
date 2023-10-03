using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrqaDB.Domain.Models
{
    public class Role
    {
        public Guid Id { get; set; }

        public string RoleName { get; set; }

        public string RoleDescription { get; set; }
    }
}
