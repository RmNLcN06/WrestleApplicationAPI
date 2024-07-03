using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WrestleApplicationAPI.Models.City;

namespace WrestleApplicationAPI.Models.Country
{
    public class CountryDTO
    {
        public int IdCountry { get; set; }

        public string FullNameCountry { get; set; } = string.Empty;

        public string ShortNameCountry { get; set; } = string.Empty;

        public string? UrlFlagCountry { get; set; }

        public int NumberOfCities
        {
            get
            {
                return Cities.Count;
            }
        }

        public ICollection<CityDTO> Cities { get; set; } = new List<CityDTO>();

    }
}
