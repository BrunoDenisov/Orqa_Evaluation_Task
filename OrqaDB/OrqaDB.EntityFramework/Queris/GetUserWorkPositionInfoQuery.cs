using OrqaDB.Domain.Models;
using OrqaDB.EntityFramework.DTOs;
using System;
using System.Linq;

namespace OrqaDB.EntityFramework.Queris
{
    public class GetUserWorkPositionInfoQuery
    {
        private readonly OrqaDbContext _dbContext;

        public GetUserWorkPositionInfoQuery(OrqaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UserWorkPositionInfo GetUserWorkPositionInfo(string username)
        {
            var userWorkPositionInfo = (from userWorkPosition in _dbContext.UserWorkPositions
                                        join user in _dbContext.Users
                                        on userWorkPosition.UserId equals user.Id
                                        where userWorkPosition.Username == username
                                        select new UserWorkPositionInfo
                                        {
                                            FirstName = user.FirstName,
                                            LastName = user.LastName,
                                            WorkPosition = userWorkPosition.WorkPosition,
                                            Date = userWorkPosition.Date
                                        }).FirstOrDefault();

            return userWorkPositionInfo;
        }
    }
}
