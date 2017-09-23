using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Psychogra.Controllers;

namespace Psycogra.CoreWeb.Controllers
{
    public interface ICategoryStore
    {
        IEnumerable<Category> Categories();
    }

    public class DiskCategoryStore : ICategoryStore
    {
        private readonly string _path;

        public DiskCategoryStore(string path)
        {
            _path = path;
        }

        public IEnumerable<Category> Categories()
        {
            var directories = Directory.GetDirectories(_path);

            return directories.Select(d => new DirectoryInfo(d)).Select((d, i) => new Category
            {
                CategoryId = d.FullName,
                Label = d.Name,
                Options = Directory.EnumerateFiles(d.FullName).Select(f => new FileInfo(f)).Select(f => new Option
                {
                    OptionId = f.FullName,
                    Label = f.Name,
                    ImageUrl = f.FullName
                })
            });
        }
    }

    public interface IGameStore
    {
        void Save<T>(T game);
        T Load<T>(T game);
    }

    public class DataStore
    {
        private readonly IEnumerable<Category> CategoryOptions = new[]
        {
            new Category
            {
                CategoryId = 1.ToString(),
                Label = "Animals",
                Options = new[]
                {
                    new Option
                    {
                        OptionId = 1.ToString(),
                        Label = "Cat",
                        ImageUrl = "http://www.petmd.com/sites/default/files/what-does-it-mean-when-cat-wags-tail.jpg"
                    },
                    new Option
                    {
                        OptionId = 2.ToString(),
                        Label = "Dog",
                        ImageUrl = "https://www.cesarsway.com/sites/newcesarsway/files/styles/large_article_preview/public/Common-dog-behaviors-explained.jpg?itok=FSzwbBoi"
                    },
                    new Option
                    {
                        OptionId = 3.ToString(),
                        Label = "Horse",
                        ImageUrl = "http://assets.atlasobscura.com/article_images/40500/image.jpg"
                    }
                }
            },
            new Category
            {
                CategoryId = 2.ToString(),
                Label = "Transport",
                Options = new[]
                {
                    new Option
                    {
                        OptionId = 4.ToString(),
                        Label = "Plane",
                        ImageUrl = "http://i2.cdn.cnn.com/cnnnext/dam/assets/160302135133-plane-spotting-a320-super-169.jpg"
                    },
                    new Option
                    {
                        OptionId = 5.ToString(),
                        Label = "Bike",
                        ImageUrl = "http://thesweethome.com/wp-content/uploads/sites/3/2017/03/hybrid-bike-lowres-2476.jpg"
                    },
                    new Option
                    {
                        OptionId = 6.ToString(),
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