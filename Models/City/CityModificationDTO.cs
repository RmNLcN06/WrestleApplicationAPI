using System.ComponentModel.DataAnnotations;

namespace WrestleApplicationAPI.Models.City
{
    public class CityModificationDTO
    {
        [Required(ErrorMessage = "You should provide a Name value.")]
        [MaxLength(150)]
        public string NameCity { get; set; } = string.Empty;
    }
}
