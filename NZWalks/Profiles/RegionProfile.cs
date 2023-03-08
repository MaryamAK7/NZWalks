using AutoMapper;

namespace NZWalks.Profiles
{
    public class RegionProfile:Profile
    {
        public RegionProfile()
        {
            CreateMap<Models.Domain.Region, Models.DTO.Region>().ReverseMap();
                //.ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id));
        }
    }
}
