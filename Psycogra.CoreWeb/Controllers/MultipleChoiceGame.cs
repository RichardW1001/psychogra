using System.Collections.Generic;
using Psychogra.Controllers;

namespace Psycogra.CoreWeb.Controllers
{
    public class MultipleChoiceGame
    {
        public string GameId { get; set; }

        public Option PrimaryOption { get; set; }
        public IEnumerable<Option> Options { get; set; }
    }
}