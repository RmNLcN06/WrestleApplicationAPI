using AutoMapper;

namespace WrestleApplicationAPI.Profiles
{
    public class CountryProfile : Profile
    {
        public CountryProfile() 
        { 
            CreateMap<Entities.Country, Models.CountryDTO>();
            CreateMap<Models.CountryCreationDTO, Entities.Country>();
        }
    }
}
