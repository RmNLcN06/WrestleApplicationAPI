namespace WrestleApplicationAPI.Models
{
    public class ContinentDTO
    {
        public int IdContinent { get; set; }

        public string NameContinent { get; set; } = string.Empty;

        public int NumberOfCountries
        {
            get
            {
                return Countries.Count;
            }
        }

        public ICollection<CountryDTO> Countries { get; set; } = new List<CountryDTO>();
    }
}
