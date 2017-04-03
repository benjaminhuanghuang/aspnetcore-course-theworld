using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
        IEnumerable<Trip> GetAllTripsWithStops();
        IEnumerable<Trip> GetUserTripsWithStops(string userName);
        Trip GetTripByName(string tripName, string userName);

        void AddTrip(Trip trip);
        void AddStop(string tripName,string userName, Stop newStop);

        Task<bool> SaveChangesAsync();
    }
}