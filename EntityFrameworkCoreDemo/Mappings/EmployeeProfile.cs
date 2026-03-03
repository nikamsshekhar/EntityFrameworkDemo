using AutoMapper;

namespace EntityFrameworkCoreDemo.Mappings
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Models.Employee, EntityFrameworkCore.Domain.Entities.Employee>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Manager, opt => opt.Ignore())
            .ForMember(dest => dest.Organization, opt => opt.Ignore())
            .ForMember(dest => dest.Address, opt => opt.Ignore());

            //Read
            CreateMap<EntityFrameworkCore.Domain.Entities.Employee, Models.EmployeeResponse>()
            .ForMember(
                dest => dest.FullName,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")
            )
            .ForMember(
                dest => dest.ManagerName,
                opt => opt.MapFrom(src =>
                    src.Manager != null
                        ? $"{src.Manager.FirstName} {src.Manager.LastName}"
                        : null
                )
            );
        }
    }
}
