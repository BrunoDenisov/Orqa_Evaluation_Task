using System;
using System.Linq;
using System.Threading.Tasks;
using OrqaDB.Domain.Models; 
using OrqaDB.EntityFramework; 

namespace OrqaDB.EntityFramework.Queries
{
    public class UpdateUserWorkPositionQuery
    {
        private readonly OrqaDbContext _dbContext;

        public UpdateUserWorkPositionQuery(OrqaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task UpdateWorkPositionAsync(string username, string selectedWorkPosition) // Updates the selected user work position
        {
            try
            {
                var userWorkPosition = _dbContext.UserWorkPositions.FirstOrDefault(uwp => uwp.Username == username);
                if (userWorkPosition != null)
                {
                    var workPosition = _dbContext.WorkPositions.FirstOrDefault(wp => wp.PositionName == selectedWorkPosition);
                    if (workPosition != null)
                    {
                        userWorkPosition.WorkPositionId = workPosition.Id;
                        userWorkPosition.WorkPosition = workPosition.PositionName;

                        userWorkPosition.Date = DateTime.Now;

                        await _dbContext.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while updating the work position: " + ex.Message);
            }
        }

    }
}

