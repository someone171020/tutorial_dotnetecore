using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using hwapp.Controllers.Resources;
using hwapp.Core.Models;
using hwapp.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hwapp.Controllers
{
    public class MakesController : Controller
    {
        private readonly HelloDbContext context;
        private readonly IMapper mapper;

        public MakesController(HelloDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet("/api/makes")]
        public async Task<IEnumerable<MakeResource>> getMakes()
        {
            var makes = await context.Makes.Include(m => m.Models).ToListAsync();

            return mapper.Map<List<Make>, List<MakeResource>>(makes);

        }
    }
}