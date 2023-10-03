using System;
using System.Linq;
using OrqaDB.Domain.Models; 

namespace OrqaDB.EntityFramework.Queries
{
    public class AuthenticationQuery
    {
        private readonly OrqaDbContext _dbContext;

        public AuthenticationQuery(OrqaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string AuthenticateUser(string username, string hashedPassword)
        {
            try
            {
                var user = _dbContext.Users.SingleOrDefault(u => u.Username == username && u.Password == hashedPassword); // Finds user with username and the hashed passord

                if (user != null) // If the user is found searches for role and returnes the role name
                {
                    var roleName = _dbContext.Roles.Single(r => r.Id == user.RoleId).RoleName;
                    return roleName;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Authentication failed.", ex);
            }
        }
    }
}
