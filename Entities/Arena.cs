using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WrestleApplicationAPI.Entities
{
    public class Arena(string nameArena)
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int IdArena { get; set; }

        [Required]
        [MaxLength(100)]
        public string NameArena { get; set; } = nameArena;


        [ForeignKey("CityId")]
        public int CityId { get; set; }
        public City? City { get; set; }
    }
}
