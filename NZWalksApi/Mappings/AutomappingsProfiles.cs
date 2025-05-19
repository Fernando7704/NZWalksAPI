using AutoMapper;
using NZWalksApi.Models.Domain;
using NZWalksApi.Models.DTO;

namespace NZWalksApi.Mappings
{
    public class AutomappingsProfiles: Profile
    {
        public AutomappingsProfiles()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<Region, addRegionRequestDto>().ReverseMap();
            CreateMap<RegionDTO, addRegionRequestDto>().ReverseMap();
            CreateMap<Walk, addRequestWalkDTO>().ReverseMap();
            CreateMap<Walk,WalksDTO>().ReverseMap();
            CreateMap<Difficulty,DifficultyDTO>().ReverseMap();
            CreateMap<UpdateWalkRequestDTO,Walk>().ReverseMap();
        }
    }
}
