﻿using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TheWorld.Models
{
    public class WorldRepository : IWorldRepository
    {
        private WorldContext _context;
        private ILogger<WorldRepository> _loger;

        public WorldRepository(WorldContext context, ILogger<WorldRepository> logger)
        {
            _context = context;
            _loger = logger;
        }

        public void AddTrip(Trip newTrip)
        {
            _context.Add(newTrip);

        }

        public IEnumerable<Trip> GetAllTrips()
        {
            try {
                return _context.Trips.OrderBy(t => t.Name).ToList();
            }
            catch (Exception ex)
            {
                _loger.LogError("Could not get trips from database", ex);
                return null;
            }

        }

        public IEnumerable<Trip> GetAllTripsWithStops()
        {
            try
            {
                return _context.Trips.Include(t => t.Stops).OrderBy(t => t.Name).ToList();
            }
            catch (Exception ex)
            {

                _loger.LogError("Could not get trips with stops from database", ex);
                return null;
            }

        }

        public bool SaveAll()
        {
           return _context.SaveChanges() > 0;
        }
    }
}
