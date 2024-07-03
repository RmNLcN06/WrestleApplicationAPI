using System.ComponentModel.DataAnnotations;

namespace WrestleApplicationAPI.Models.Country
{
    public class CountryCreationDTO
    {
        [Required(ErrorMessage = "You should provide a Full Name value.")]
        [MaxLength(170)]
        public string FullNameCountry { get; set; } = string.Empty;

        [Required(ErrorMessage = "You should provide a Short Name value.")]
        [MaxLength(3)]
        public string ShortNameCountry { get; set; } = string.Empty;

        [Required(ErrorMessage = "You should provide an URL Flag value.")]
        [MaxLength(255)]
        public string? UrlFlagCountry { get; set; }
    }
}
