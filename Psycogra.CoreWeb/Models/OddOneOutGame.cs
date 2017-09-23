using System.Collections.Generic;

namespace Psychogra.Controllers
{
    public class OddOneOutGame
    {
        public int GameId { get; set; }
        public IEnumerable<Option> Options { get; set; }
    }
}