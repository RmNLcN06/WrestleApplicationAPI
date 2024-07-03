using System.ComponentModel.DataAnnotations;

namespace WrestleApplicationAPI.Models.City
{
    public class CityDTO
    {
        public int IdCity { get; set; }

        public string NameCity { get; set; } = string.Empty;
    }
}
