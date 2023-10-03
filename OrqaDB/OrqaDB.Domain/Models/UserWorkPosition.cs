using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace OrqaDB.Domain.Models
{
    public class UserWorkPosition
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string WorkPosition { get; set; }

        public string ProductName { get; set; }

        public DateTime Date { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid WorkPositionId { get; set; }
        public WorkPosition WorkPositionRef { get; set; }

    }
}
