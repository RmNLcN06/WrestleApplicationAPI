using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WrestleApplicationAPI.Entities
{
    public class Country
    {
        [Key]
        [Required]
        [MaxLength(3)]
        public string IdCountry { get; set; }

        [Required]
        [MaxLength(170)]
        public string NameCountry { get; set; }

        [Required]
        [MaxLength(255)]
        public string? UrlFlagCountry { get; set; }

        [ForeignKey("ContinentId")]
        public int ContinentId { get; set; }
        public Continent? Continent { get; set; }
        
    }
}
