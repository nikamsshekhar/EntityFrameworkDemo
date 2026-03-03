using AutoMapper;

namespace EntityFrameworkCoreDemo.Mappings
{
    public class AddressProfile: Profile
    {
        public AddressProfile()
        {
            CreateMap<EntityFrameworkCore.Domain.Entities.Address, Models.Address>();
        }
    }
}
