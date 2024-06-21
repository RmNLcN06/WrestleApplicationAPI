using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrestleApplicationAPI.Entities
{
    public class Continent(string nameContinent)
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int IdContinent { get; set; }

        [Required]
        [MaxLength(15)]
        public string NameContinent { get; set; } = nameContinent;

        public ICollection<Country> Countries { get; set; } = new List<Country>();

    }
}
