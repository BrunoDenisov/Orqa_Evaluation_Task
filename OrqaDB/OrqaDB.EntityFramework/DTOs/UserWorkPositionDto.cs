using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace OrqaDB.EntityFramework.DTOs
{
    public class UserWorkPositionDto
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(255)]
        public string Username { get; set; }

        [MaxLength(255)]
        public string WorkPosition { get; set; }

        [MaxLength(255)]
        public string ProductName { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public virtual UserDto User { get; set; }

        [ForeignKey("WorkPosition")]
        public Guid WorkPositionId { get; set; }

        public WorkPsitionDto WorkPositionReference { get; set; }
    }
}
