using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrestleApplicationAPI.Models
{
    public class CountryDTO
    {
        public string IdCountry { get; set; }

        public string NameCountry { get; set; } = string.Empty;

        public string? UrlFlagCountry { get; set; }

    }
}
