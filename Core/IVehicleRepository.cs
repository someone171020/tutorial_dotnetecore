using System.Threading.Tasks;
using hwapp.Core.Models;

namespace hwapp.Core
{
    public interface IVehicleRepository
    {
         Task<Vehicle> GetVehicle(int id, bool includeRelated=true);
         void Add(Vehicle vehicle);
         void Remove(Vehicle vehicle);
    }
}