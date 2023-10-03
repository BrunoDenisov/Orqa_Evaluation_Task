using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrqaDB.EntityFramework.Queris
{
    public class FetchAvailableRolesFromDB : IDisposable
    {
        private readonly OrqaDbContext _dbContext;

        public FetchAvailableRolesFromDB(OrqaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<string> GetAvailableRoles()
        {
            var availableRoles = _dbContext.Roles.Select(role => role.RoleName).ToList();

            return new List<string>(availableRoles);
        }

        public void Dispose()
        {
            
        }
    }
}
