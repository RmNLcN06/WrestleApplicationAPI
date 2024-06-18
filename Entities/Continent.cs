using System.ComponentModel.DataAnnotations;

namespace WrestleApplicationAPI.Entities
{
    public class Continent
    {
        [Key]
        public int IdContinent { get; set; }

        [Required]
        [MaxLength(15)]
        public string NameContinent { get; set; }

        public ICollection<Country> Countries { get; set; } = new List<Country>();
    }
}
