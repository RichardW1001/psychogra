using System.Collections.Generic;
using Psychogra.Controllers;

namespace Psycogra.CoreWeb.Controllers
{
    public class DataStore
    {
        public readonly IEnumerable<Category> CategoryOptions = new[]
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

        public static readonly List<OddOneOutGame> OddOneOutGames = new List<OddOneOutGame>();

        public static readonly List<MultipleChoiceGame> MultipleChoiceGames = new List<MultipleChoiceGame>();
    }
}