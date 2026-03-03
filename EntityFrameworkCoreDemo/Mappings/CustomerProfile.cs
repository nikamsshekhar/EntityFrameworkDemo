using AutoMapper;

namespace EntityFrameworkCoreDemo.Mappings
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Models.Customer, EntityFrameworkCore.Domain.Entities.Customer>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Organization, opt => opt.Ignore())
            .ForMember(dest => dest.Address, opt => opt.Ignore());
            
            CreateMap<EntityFrameworkCore.Domain.Entities.Customer, Models.CustomerResponse>()
                .ForMember(
                dest => dest.FullName,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")
            );
        }
    }
}
