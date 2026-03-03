using AutoMapper;

namespace EntityFrameworkCoreDemo.Mappings
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<Models.Organization, EntityFrameworkCore.Domain.Entities.Organization>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Employees, opt => opt.Ignore())
            .ForMember(dest => dest.Customers, opt => opt.Ignore())
            .ForMember(dest => dest.Address, opt => opt.Ignore());

            CreateMap<EntityFrameworkCore.Domain.Entities.Organization, Models.OrganizationResponse>();
        }
    }
}
