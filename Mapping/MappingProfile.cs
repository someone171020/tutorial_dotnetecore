using System.Linq;
using AutoMapper;
using hwapp.Controllers.Resources;
using hwapp.Models;

namespace hwapp.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to API resources
            CreateMap<Make, MakeResource>();
            CreateMap<Model, ModelResource>();
            CreateMap<Vehicle, VehicleResource>()
                .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource{ Name = v.contactName, Email = v.contactEmail, Phone = v.contactPhone}))
                .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => vf.FeatureId)));

            // API resource to Domain
            CreateMap<VehicleResource, Vehicle>()
                .ForMember(v => v.contactName, opt => opt.MapFrom(vr => vr.Contact.Name))
                .ForMember(v => v.contactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
                .ForMember(v => v.contactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
                .ForMember(v => v.Features, opt => opt.MapFrom(vr => vr.Features.Select(id => new VehicleFeature{ FeatureId = id})));
        }
    }
}