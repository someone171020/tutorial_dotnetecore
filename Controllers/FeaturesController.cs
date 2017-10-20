using System.Collections.Generic;
using System.Threading.Tasks;
using hwapp.Models;
using hwapp.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hwapp.Controllers
{
    public class FeaturesController : Controller
    {
        private readonly HelloDbContext context;
        public FeaturesController(HelloDbContext context)
        {
            this.context = context;    
        }
        
        [HttpGet("/api/features")]
        public async Task<IEnumerable<Feature>> getFeatures()
        {
            return await context.Features.ToListAsync();
        }
    }
}