using System;
using System.Threading.Tasks;
using AutoMapper;
using hwapp.Controllers.Resources;
using hwapp.Models;
using hwapp.Persistence;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] VehicleResource vehicleResource)
        {
            if(!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            // var model = await context.Models.FindAsync(vehicleResource.ModelId);
            // if(model == null) {
            //     ModelState.AddModelError("ModelId", "Invalid model ID");
            //     return BadRequest(ModelState);
            // }

            var vehicle = mapper.Map<VehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.Now;
            this.context.Add(vehicle);
            await this.context.SaveChangesAsync();

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }
    }
}