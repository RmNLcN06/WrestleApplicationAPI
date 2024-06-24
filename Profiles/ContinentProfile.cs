using AutoMapper;

namespace WrestleApplicationAPI.Profiles
{
    public class ContinentProfile : Profile
    {
        public ContinentProfile() 
        { 
            CreateMap<Entities.Continent, Models.ContinentWithoutCountriesDTO>();
        }
    }
}
