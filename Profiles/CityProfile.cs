using AutoMapper;
using WrestleApplicationAPI.Models.City;

namespace WrestleApplicationAPI.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<Entities.City, CityDTO>();
            CreateMap<CityCreationDTO, Entities.City>();
            CreateMap<CityModificationDTO, Entities.City>();
            CreateMap<Entities.City, CityModificationDTO>();
        }
    }
}
