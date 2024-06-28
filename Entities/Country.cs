using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WrestleApplicationAPI.Entities
{
    public class Country(string fullNameCountry)
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int IdCountry { get; set; }

        [Required]
        [MaxLength(170)]
        public string FullNameCountry { get; set; } = fullNameCountry;

        [Required]
        [MaxLength(3)]
        public string ShortNameCountry { get; set; }

        [Required]
        [MaxLength(255)]
        public string? UrlFlagCountry { get; set; }



        [ForeignKey("ContinentId")]
        public int ContinentId { get; set; }
        public Continent? Continent { get; set; }


        public ICollection<City> Cities { get; set; } = new List<City>();

    }
}
