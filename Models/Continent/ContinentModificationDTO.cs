using System.ComponentModel.DataAnnotations;

namespace WrestleApplicationAPI.Models.Continent
{
    public class ContinentModificationDTO
    {
        [Required(ErrorMessage = "You should provide a Name value.")]
        [MaxLength(15)]
        public string NameContinent { get; set; } = string.Empty;
    }
}
