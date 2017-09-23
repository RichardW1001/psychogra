using System.Collections.Generic;

namespace Psychogra.Controllers
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Label { get; set; }
        public IEnumerable<Option> Options { get; set; }
    }
}