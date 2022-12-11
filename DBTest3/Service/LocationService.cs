using DBTest3.Data;
using DBTest3.Data.Entity;

namespace DBTest3.Service
{
    public class LocationService : ILocationService
    {
        private readonly ApplicationDbContext context;

        public LocationService(ApplicationDbContext applicationDbContext)
        {
            this.context = applicationDbContext;
        }

        public List<Location> GetLocations()
        {
            return context.locations.ToList();
        }

        public void saveLocation(Location location)
        {
            context.Add(location);
            context.SaveChanges();
        }
    }
}
