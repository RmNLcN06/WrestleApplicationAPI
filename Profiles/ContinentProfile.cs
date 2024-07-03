using AutoMapper;
using WrestleApplicationAPI.Models.Continent;

namespace WrestleApplicationAPI.Profiles
{
    public class ContinentProfile : Profile
    {
        public ContinentProfile() 
        { 
            CreateMap<Entities.Continent, ContinentWithoutCountriesDTO>();
            CreateMap<Entities.Continent, ContinentDTO>();
        }
    }
}
