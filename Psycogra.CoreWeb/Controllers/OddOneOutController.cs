using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Psychogra.Controllers;

namespace Psycogra.CoreWeb.Controllers
{
    public class OddOneOutController : Controller
    {
        private static int _nextGameId = 1;

        private readonly DataStore _dataStore = new DataStore();

        [HttpGet]
        [Route("api/games/oddoneout/new")]
        public OddOneOutGame NewGame()
        {
            var setSize = 4;

            var mainSet = Enumerable.OrderBy<Category, Guid>(_dataStore.CategoryOptions, x => Guid.NewGuid()).
                First();

            var oddOneOut = Enumerable.Where<Category>(_dataStore.CategoryOptions, c => c.CategoryId != mainSet.CategoryId).
                SelectMany(c => c.Options).
                OrderBy(x => Guid.NewGuid()).
                First();

            _nextGameId++;

            var newGame = new OddOneOutGame
            {
                GameId = _nextGameId,
                Options = mainSet.Options.
                    OrderBy(x => Guid.NewGuid()).Take(setSize - 1).
                    Union(new[] { oddOneOut }).
                    OrderBy(x => Guid.NewGuid()).ToArray()
            };

            DataStore.OddOneOutGames.Add(newGame);

            return newGame;
        }

        public class GuessViewModel
        {
            public int GameId { get; set; }
            public int OptionId { get; set; }
        }

        [HttpPost]
        [Route("api/games/oddoneout/guess")]
        public bool Guess(GuessViewModel model)
        {
            //look up game
            //look up the option, and which set it's from

            var sets = Enumerable.SelectMany(_dataStore.CategoryOptions, c => Enumerable.Select(c.Options, o => new { CategoryId = c.CategoryId, OptionId = o.OptionId })).ToList();

            var game = Enumerable.First<OddOneOutGame>(DataStore.OddOneOutGames, g => g.GameId == model.GameId);

            var gameSets = game.Options.
                Join(sets, o => o.OptionId, x => x.OptionId, (o, x) => x.CategoryId).
                GroupBy<int, int>(x => x);

            var chosenOption = sets.First(x => x.OptionId == model.OptionId);

            return gameSets.First(x => x.First() == chosenOption.CategoryId).Count() == 1;
        }
    }
}