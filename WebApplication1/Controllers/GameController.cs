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
                        Label = "Cat",
                        ImageUrl = "http://www.petmd.com/sites/default/files/what-does-it-mean-when-cat-wags-tail.jpg"
                    },
                    new Option
                    {
                        OptionId = 2,
                        Label = "Dog",
                        ImageUrl = "https://www.cesarsway.com/sites/newcesarsway/files/styles/large_article_preview/public/Common-dog-behaviors-explained.jpg?itok=FSzwbBoi"
                    },
                    new Option
                    {
                        OptionId = 3,
                        Label = "Horse",
                        ImageUrl = "http://assets.atlasobscura.com/article_images/40500/image.jpg"
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
                        Label = "Plane",
                        ImageUrl = "http://i2.cdn.cnn.com/cnnnext/dam/assets/160302135133-plane-spotting-a320-super-169.jpg"
                    },
                    new Option
                    {
                        OptionId = 5,
                        Label = "Bike",
                        ImageUrl = "http://thesweethome.com/wp-content/uploads/sites/3/2017/03/hybrid-bike-lowres-2476.jpg"
                    },
                    new Option
                    {
                        OptionId = 6,
                        Label = "Car",
                        ImageUrl = "http://images.car.bauercdn.com/pagefiles/70980/001.jpg"
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