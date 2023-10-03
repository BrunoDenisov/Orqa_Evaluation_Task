using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrqaDB.EntityFramework.DTOs
{
    public class UserDto
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(255)]
        public string FirstName { get; set; }

        [MaxLength(255)]
        public string LastName { get; set; }

        [MaxLength(255)]
        public string Username { get; set; }

        [MaxLength(255)]
        public string Password { get; set; }

        [ForeignKey("Role")]
        public Guid RoleId { get; set; }

        public virtual RoleDto Role { get; set; }
    }
}
