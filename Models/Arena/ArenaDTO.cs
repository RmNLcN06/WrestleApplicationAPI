using System.ComponentModel.DataAnnotations;

namespace WrestleApplicationAPI.Models.Arena
{
    public class ArenaDTO
    {
        public int IdArena { get; set; }

        public string NameArena { get; set; } = string.Empty;
    }
}
