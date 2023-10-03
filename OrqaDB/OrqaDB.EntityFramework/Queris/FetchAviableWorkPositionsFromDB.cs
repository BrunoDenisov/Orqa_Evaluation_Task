using OrqaDB.Domain.Models;
using OrqaDB.EntityFramework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OrqaDB.EntityFramework.Queris
{
    public class FetchAviableWorkPositionsFromDB : IDisposable
    {
        private readonly OrqaDbContext _dbContext;

        public FetchAviableWorkPositionsFromDB(OrqaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<WorkPosition> GetAvailableWorkPositions()
        {
            var availableWorkPositions = _dbContext.WorkPositions.ToList();

            return new List<WorkPosition>(availableWorkPositions);
        }


        public void Dispose()
        {

        }
    }
}
