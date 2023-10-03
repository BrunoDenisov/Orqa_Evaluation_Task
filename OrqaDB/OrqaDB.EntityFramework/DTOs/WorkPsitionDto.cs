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
    public class WorkPsitionDto
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(255)]
        public string PositionName { get; set; }

        [MaxLength(255)]
        public string PositionDescription { get; set; }
    }
}
