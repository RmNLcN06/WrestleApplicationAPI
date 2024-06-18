namespace WrestleApplicationAPI.Entities
{
    public class Continent
    {
        public int IdContinent { get; set; }

        public string NameContinent { get; set; }

        public ICollection<Country> Countries { get; set; } = new List<Country>();
    }
}
