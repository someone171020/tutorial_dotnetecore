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
            CreateMap<Vehicle, SaveVehicleResource>()
                .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource{ Name = v.contactName, Email = v.contactEmail, Phone = v.contactPhone}))
                .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => vf.FeatureId)));
            CreateMap<Vehicle, VehicleResource>()
                .ForMember(vr => vr.Make, opt => opt.MapFrom(v => v.Model.Make))
                .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource{ Name = v.contactName, Email = v.contactEmail, Phone = v.contactPhone}))
                .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => new Feature{ Id = vf.Feature.Id, Name = vf.Feature.Name})));

            // API resource to Domain
            CreateMap<SaveVehicleResource, Vehicle>()
                .ForMember(v => v.Id, opt => opt.Ignore())
                .ForMember(v => v.contactName, opt => opt.MapFrom(vr => vr.Contact.Name))
                .ForMember(v => v.contactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
                .ForMember(v => v.contactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
                .ForMember(v => v.Features, opt => opt.Ignore())
                .AfterMap((vr, v) => {                    
                    // remove unselected features
                    // var removedFeatures = new System.Collections.Generic.List<VehicleFeature>();
                    // foreach (var f in v.Features)
                    //     if (!vr.Features.Contains(f.FeatureId))
                    //         removedFeatures.Add(f);
                    var removedFeatures = v.Features.Where(f => !vr.Features.Contains(f.FeatureId));
                    foreach (var f in removedFeatures)
                        v.Features.Remove(f);

                    // add new features
                    // foreach (var id in vr.Features)
                    //     if (!v.Features.Any(f => f.FeatureId == id))
                    //         v.Features.Add(new VehicleFeature {FeatureId = id});
                    
                    var addedFeatures = vr.Features.Where(id => !v.Features.Any(f => f.FeatureId == id)).Select(id => new VehicleFeature{FeatureId = id});
                    foreach (var f in addedFeatures)
                        v.Features.Add(f);
                });
        }
    }
}