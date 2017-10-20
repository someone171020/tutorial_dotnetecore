using AutoMapper;
using hwapp.Controllers.Resources;
using hwapp.Models;

namespace hwapp.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Make, MakeResource>();
            CreateMap<Model, ModelResource>();
        }
    }
}