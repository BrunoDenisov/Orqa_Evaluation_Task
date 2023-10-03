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
            var userRole = dbContext.Roles.FirstOrDefault(r => r.RoleName == roleName); // Fetching role with the proved roleName
            if (userRole == null)
            {
                Console.WriteLine("Error: User role not found.");
                return;
            }

            user.Id = Guid.NewGuid();

            string hexGuid = ConvertGuidToHex(user.Id);

            user.Id = new Guid(hexGuid);

            user.RoleId = userRole.Id;

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        private static string ConvertGuidToHex(Guid guid) // Method for converting GUID into a hex representation without hyphens
        {
            return guid.ToString("N");
        }
    }
}
