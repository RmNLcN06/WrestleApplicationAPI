using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WrestleApplicationAPI.Models.City
{
    public class CityCreationDTO
    {
        [Required(ErrorMessage = "You should provide a Name value.")]
        [MaxLength(150)]
        public string NameCity { get; set; } = string.Empty;
    }
}
