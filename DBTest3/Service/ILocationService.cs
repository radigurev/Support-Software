using DBTest3.Data.Entity;

namespace DBTest3.Service
{
    public interface ILocationService
    {
        public List<Location> GetLocations();

        void saveLocation(Location location);
    }
}
