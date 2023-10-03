using System;
using System.Linq;
using System.Threading.Tasks;
using OrqaDB.Domain.Models;

namespace OrqaDB.EntityFramework.Queris
{
    public class InsertUserQuery
    {
        public static async Task InsertUserAsync(User user, OrqaDbContext dbContext, string roleName)
        {
            // Fetch the role based on the provided roleName
            var userRole = dbContext.Roles.FirstOrDefault(r => r.RoleName == roleName);
            if (userRole == null)
            {
                Console.WriteLine("Error: User role not found.");
                return;
            }

            // Generate a new GUID
            user.Id = Guid.NewGuid();

            // Convert GUID to binary (little-endian format as required by SQL Server)
            var guidBytes = user.Id.ToByteArray();
            Array.Reverse(guidBytes); // Reverse for little-endian format
            user.IdBinary = guidBytes;

            user.RoleId = userRole.Id;  // Assign the role ID

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
