using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WrestleApplicationAPI.Entities
{
    public class Country
    {
        [Key]
        public string IdCountry { get; set; }

        public string NameCountry { get; set; }

        public string? UrlFlagCountry { get; set; }

        [ForeignKey("ContinentId")]
        public Continent? Continent { get; set; }
        public int ContinentId { get; set; }
    }
}
