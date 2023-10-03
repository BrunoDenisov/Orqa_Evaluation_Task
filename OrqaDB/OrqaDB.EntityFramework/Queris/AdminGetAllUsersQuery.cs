using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using OrqaDB.Domain.Models;
using OrqaDB.EntityFramework.DTOs;

namespace OrqaDB.EntityFramework.Queris
{
    public class AdminGetAllUsersQuery
    {
        private readonly OrqaDbContext _dbContext;

        public AdminGetAllUsersQuery(OrqaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<UserWorkPositionDto> GetAllUsers()
        {
            var query = _dbContext.UserWorkPositions
                .Include(uwp => uwp.User) 
                .Include(uwp => uwp.WorkPosition) 
                .Select(userWorkPosition => new UserWorkPositionDto
                {
                    Id = userWorkPosition.Id,
                    Username = userWorkPosition.User.Username,
                    WorkPosition = userWorkPosition.WorkPositionRef.PositionName,
                    Date = userWorkPosition.Date,
                    User = new UserDto
                    {
                        FirstName = userWorkPosition.User.FirstName,
                        LastName = userWorkPosition.User.LastName,
                        Username = userWorkPosition.User.Username,
                    },
                    WorkPositionReference = new WorkPsitionDto
                    {
                        PositionName = userWorkPosition.WorkPositionRef.PositionName,
                    }
                });

            return query.ToList();
        }
    }
}
