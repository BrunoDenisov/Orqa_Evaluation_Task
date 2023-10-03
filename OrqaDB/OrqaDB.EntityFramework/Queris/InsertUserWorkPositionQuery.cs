using OrqaDB.Domain.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OrqaDB.EntityFramework.Queries
{
    public static class InsertUserWorkPositionQuery
    {
        public static async Task InsertUserWorkPositionAsync(OrqaDbContext dbContext, string username, WorkPosition workPosition, string productName) // Creates a UserWorkPostion in the data base when a user is create and is assigned a work postion
        {
            var user = dbContext.Users.SingleOrDefault(u => u.Username == username);

            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            var userWorkPosition = new UserWorkPosition
            {
                Id = Guid.NewGuid(),
                Username = username,
                WorkPosition = workPosition.PositionName,
                ProductName = productName,
                Date = DateTime.UtcNow,
                UserId = user.Id,
                WorkPositionId = (dbContext.WorkPositions.SingleOrDefault(wp => wp.PositionName == workPosition.PositionName)).Id
            };

            dbContext.UserWorkPositions.Add(userWorkPosition);
            await dbContext.SaveChangesAsync();
        }
    }
}
