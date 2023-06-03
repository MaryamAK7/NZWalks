using AutoMapper;

namespace NZWalks.Profiles
{
    public class WalkProfile : Profile
    {
        public WalkProfile() {
            CreateMap<Models.Domain.Walk, Models.DTO.Walk>().ReverseMap();
            CreateMap<Models.Domain.WalkDifficulty, Models.DTO.WalkDifficulty>().ReverseMap();
        
        }

    }
}
