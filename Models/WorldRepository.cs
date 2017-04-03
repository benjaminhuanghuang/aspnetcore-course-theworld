using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TheWorld.Models;

namespace TheWorld.Models
{
    public class WorldRepository : IWorldRepository
    {
        private WorldContext _context;
        private ILogger<WorldRepository> _logger;

        public WorldRepository(WorldContext context, ILogger<WorldRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            _logger.LogInformation("Getting all trips from the database");
            return _context.Trips.ToList();
        }

        public Trip GetTripByName(string tripName, string userName)
        {
            return _context.Trips.Include(t => t.Stops)
                                 .Where(t => t.Name == tripName && t.UserName == userName)
                                 .FirstOrDefault();

        }
        public IEnumerable<Trip> GetAllTripsWithStops()
        {
            try{
             return _context.Trips.Include(t => t.Stops)
                                .OrderBy(t=>t.Name)
                                .ToList();
            }
            catch (Exception exp)
            {
                 _logger.LogError("Can not get trips with stops from database", exp);
                return null;
            }
        }
        public IEnumerable<Trip> GetUserTripsWithStops(string userName)
        {
            try{
             return _context.Trips.Include(t => t.Stops)
                                .OrderBy(t=>t.Name)
                                .Where(t => t.UserName == userName)
                                .ToList();
            }
            catch (Exception exp)
            {
                 _logger.LogError("Can not get trips with stops from database", exp);
                return null;
            }
        }
        public void AddTrip(Trip trip)
        {
            _context.Add(trip);
        }
        public void AddStop(string tripName, string userName, Stop newStop)
        {
            var trip = GetTripByName(tripName, userName);
            if(trip !=null)
            {
                trip.Stops.Add(newStop);
                _context.Stops.Add(newStop);
            }
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}