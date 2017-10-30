using System.Threading.Tasks;
using hwapp.Core;
using hwapp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace hwapp.Persistence
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly HelloDbContext context;
        public VehicleRepository(HelloDbContext context)
        {
            this.context = context;

        }
        public async Task<Vehicle> GetVehicle(int id, bool includeRelated)
        {
            if (!includeRelated)
                return await this.context.Vehicles.FindAsync(id);
            return await this.context.Vehicles
                .Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                    .ThenInclude(vm => vm.Make)
                .SingleOrDefaultAsync(v => v.Id == id);
        }

        public void Add(Vehicle vehicle)
        {
            this.context.Vehicles.Add(vehicle);
        }
        public void Remove(Vehicle vehicle)
        {
            this.context.Remove(vehicle);
        }
    }
}