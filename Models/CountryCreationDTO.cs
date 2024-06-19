using System.ComponentModel.DataAnnotations;

namespace WrestleApplicationAPI.Models
{
    public class CountryCreationDTO
    {
        [Key]
        [Required(ErrorMessage = "You should provide an ID value.")]
        [MaxLength(3)]
        public string IdCountry { get; set; }

        [Required(ErrorMessage = "You should provide a Name value.")]
        [MaxLength(170)]
        public string NameCountry { get; set; } = string.Empty;

        [Required(ErrorMessage = "You should provide an URL Flag value.")]
        [MaxLength(255)]
        public string? UrlFlagCountry { get; set; }
    }
}
