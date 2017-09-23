using System.Collections.Generic;

namespace Psychogra.Controllers
{
    public class Game
    {
        public int GameId { get; set; }
        public IEnumerable<Option> Options { get; set; }
    }
}