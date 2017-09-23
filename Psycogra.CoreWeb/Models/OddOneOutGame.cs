using System.Collections.Generic;

namespace Psychogra.Controllers
{
    public class OddOneOutGame
    {
        public string GameId { get; set; }
        public IEnumerable<Option> Options { get; set; }
    }
}