using System;
using System.Threading.Tasks;
using AutoMapper;
using hwapp.Controllers.Resources;
using hwapp.Models;
using hwapp.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hwapp.Controllers
{
    [Route("/api/vehicles")]
    public class VehicleController : Controller
    {
        private readonly IMapper mapper;
        private readonly HelloDbContext context;
        public VehicleController(IMapper mapper, HelloDbContext context)
        {
            this.context = context;
            this.mapper = mapper;

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource vehicleResource)
        {
            if(!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var vehicle = await context.Vehicles.Include(v => v.Features).SingleOrDefaultAsync(v => v.Id == id);
            // if(vehicle == null) {
            //     ModelState.AddModelError("ModelId", "Invalid model ID");
            //     return BadRequest(ModelState);
            // }
            if (vehicle == null)
                return NotFound();

            mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource, vehicle);
            vehicle.LastUpdate = DateTime.Now;
            await this.context.SaveChangesAsync();

            var result = mapper.Map<Vehicle, SaveVehicleResource>(vehicle);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await context.Vehicles.FindAsync(id);
            if (vehicle == null)
                return NotFound();

            context.Remove(vehicle);
            await context.SaveChangesAsync();

            return Ok(id);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await context.Vehicles
                .Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                    .ThenInclude(vm => vm.Make)
                .SingleOrDefaultAsync(v => v.Id == id);
            if (vehicle == null)
                return NotFound();
            
            var vehicleResource = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(vehicleResource);
        }
    }
}