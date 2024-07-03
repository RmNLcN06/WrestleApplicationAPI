using AutoMapper;
using WrestleApplicationAPI.Models.Country;

namespace WrestleApplicationAPI.Profiles
{
    public class CountryProfile : Profile
    {
        public CountryProfile() 
        { 
            CreateMap<Entities.Country, CountryDTO>();
            CreateMap<CountryCreationDTO, Entities.Country>();
            CreateMap<CountryModificationDTO, Entities.Country>();
            CreateMap<Entities.Country, CountryModificationDTO>();
        }
    }
}
