using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Psychogra.Controllers
{
    public class GameController : ApiController
    {
        private static int _nextGameId = 1;

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
        [Route("api/game/new")]
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

            _nextGameId++;

            var newGame = new Game
            {
                GameId = _nextGameId,
                Options = mainSet.Options.
                    OrderBy(x => Guid.NewGuid()).Take(setSize - 1).
                    Union(new []{ oddOneOut }).
                    OrderBy(x => Guid.NewGuid()).ToArray()
            };

            Games.Add(newGame);

            return newGame;
        }

        public class GuessViewModel
        {
            public int GameId { get; set; }
            public int OptionId { get; set; }
        }

        [HttpPost]
        [Route("api/game/guess")]
        public bool Guess(GuessViewModel model)
        {
            //look up game
            //look up the option, and which set it's from

            var sets = _categoryOptions
                .SelectMany(c => c.Options.Select(o => new {CategoryId = c.CategoryId, OptionId = o.OptionId})).ToList();

            var game = Games.First(g => g.GameId == model.GameId);

            var gameSets = game.Options.
                Join(sets, o => o.OptionId, x => x.OptionId, (o,x) => x.CategoryId).
                GroupBy(x => x);

            var chosenOption = sets.First(x => x.OptionId == model.OptionId);

            return gameSets.First(x => x.First() == chosenOption.CategoryId).Count() == 1;
        }
    }
}