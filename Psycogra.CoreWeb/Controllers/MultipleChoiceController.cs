using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Psycogra.CoreWeb.Controllers
{
    public class MultipleChoiceController : Controller
    {
        private static int _nextGameId = 1;
        private readonly ICategoryStore _categoryStore = new DiskCategoryStore(@"C:\Users\Richard\Desktop\Demo data");

        [Route("api/multiplechoice/new")]
        public MultipleChoiceGame NewGame()
        {
            _nextGameId++;

            var totalNumberOfOptions = 3;

            var category = _categoryStore.Categories().OrderBy(x => Guid.NewGuid()).
                First();

            var primaryOption = category.Options.OrderBy(x => Guid.NewGuid()).First();

            var options = category.Options.
                Where(o => o.OptionId != primaryOption.OptionId).
                OrderBy(x => Guid.NewGuid()).
                Take(totalNumberOfOptions - 1).
                Union(new []{primaryOption}).
                OrderBy(x => Guid.NewGuid()).
                ToArray();


            var game = new MultipleChoiceGame
            {
                GameId = _nextGameId.ToString(),
                PrimaryOption = primaryOption,
                Options = options
            };

            DataStore.MultipleChoiceGames.Add(game);

            return game;
        }

        [HttpPost]
        [Route("api/multiplechoice/guess")]
        public bool Guess(string gameId, string choiceId)
        {
            var game = DataStore.MultipleChoiceGames.First(g => g.GameId == gameId);

            return game.PrimaryOption.OptionId == choiceId;
        }
    }
}