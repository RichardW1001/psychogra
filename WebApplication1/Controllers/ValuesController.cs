using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class GameController : ApiController
    {
        private readonly IEnumerable<Category> _categoryOptions = new[]
        {
            new Category
            {
                CategoryId = 1,
                Label = "Animals",
                Options = new[]
                {
                    new Option
                    {
                        OptionId = 1,
                        Label = "Cat"
                    },
                    new Option
                    {
                        OptionId = 2,
                        Label = "Dog"
                    },
                    new Option
                    {
                        OptionId = 3,
                        Label = "Horse"
                    }
                }
            },
            new Category
            {
                CategoryId = 2,
                Label = "Transport",
                Options = new[]
                {
                    new Option
                    {
                        OptionId = 4,
                        Label = "Plane"
                    },
                    new Option
                    {
                        OptionId = 5,
                        Label = "Bike"
                    },
                    new Option
                    {
                        OptionId = 6,
                        Label = "Car"
                    }
                }
            }
        };

        private static readonly List<Game> Games = new List<Game>();

        [HttpGet]
        public Game NewGame()
        {
            var setSize = 4;

            var mainSet = _categoryOptions.
                OrderBy(x => Guid.NewGuid()).
                First();

            var oddOneOut = _categoryOptions.
                Where(c => c.CategoryId != mainSet.CategoryId).
                SelectMany(c => c.Options).
                OrderBy(x => Guid.NewGuid()).
                First();

            var newGame = new Game
            {
                GameId = 123,
                Options = mainSet.Options.
                    OrderBy(x => Guid.NewGuid()).Take(setSize - 1).
                    Union(new []{ oddOneOut }).
                    OrderBy(x => Guid.NewGuid()).ToArray()
            };

            Games.Add(newGame);

            return newGame;
        }

        [HttpPost]
        public bool Guess(int gameId, int optionId)
        {
            //look up game
            //look up the option, and which set it's from

            var sets = _categoryOptions
                .SelectMany(c => c.Options.Select(o => new {CategoryId = c.CategoryId, OptionId = o.OptionId})).ToList();

            var game = Games.First(g => g.GameId == gameId);

            var gameSets = game.Options.
                Join(sets, o => o.OptionId, x => x.OptionId, (o,x) => x.CategoryId).
                GroupBy(x => x);

            var chosenOption = sets.First(x => x.OptionId == optionId);

            return gameSets.First(x => x.First() == chosenOption.CategoryId).Count() == 1;
        }
    }

    public class Game
    {
        public int GameId { get; set; }
        public IEnumerable<Option> Options { get; set; }
    }

    public class Category
    {
        public int CategoryId { get; set; }
        public string Label { get; set; }
        public IEnumerable<Option> Options { get; set; }
    }

    public class Option
    {
        public int OptionId { get; set; }
        public string Label { get; set; }
    }

    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
