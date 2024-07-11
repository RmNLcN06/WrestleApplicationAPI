using AutoMapper;
using WrestleApplicationAPI.Models.Continent;
using WrestleApplicationAPI.Models.Country;

namespace WrestleApplicationAPI.Profiles
{
    public class ContinentProfile : Profile
    {
        public ContinentProfile() 
        { 
            CreateMap<Entities.Continent, ContinentWithoutCountriesDTO>();
            CreateMap<Entities.Continent, ContinentDTO>();
            CreateMap<ContinentCreationDTO, Entities.Continent>();
        }
    }
}
