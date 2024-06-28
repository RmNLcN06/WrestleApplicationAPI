using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrestleApplicationAPI.Entities
{
    public class City(string nameCity)
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int IdCity { get; set; }

        [Required]
        [MaxLength(150)]
        public string NameCity { get; set; } = nameCity;



        [ForeignKey("CountryId")]
        public int CountryId { get; set; }
        public Country? Country { get; set; }
    }
}
