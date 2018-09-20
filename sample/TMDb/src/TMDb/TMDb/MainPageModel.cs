using System.Collections.Generic;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;

namespace TMDb
{
    public class CastListModel
    {
        public IList<Cast> Items { get; set; }
    }

    public class MovieListModel
    {
        public string Title { get; set; }
        public IList<SearchMovie> Items { get; set; }
    }

    public class MenuItemModel
    {
        public string Text { get; set; }
        public MovieListModel Movies { get; set; }
    }

    public class MainPageModel
    {
        public IList<MenuItemModel> MenuItems { get; }

        public MainPageModel()
        {
            MenuItems = new List<MenuItemModel>() {
                new MenuItemModel {Text = "Now playing"},
                new MenuItemModel {Text = "Top rated"},
                new MenuItemModel {Text = "Popular"},
                new MenuItemModel {Text = "Upcoming"}
            };
        }

    }
}
