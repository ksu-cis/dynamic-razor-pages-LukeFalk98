using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movies.Pages
{
    public class IndexModel : PageModel
    {
        /// <summary>
        /// the list of movies
        /// </summary>
        public IEnumerable<Movie> Movies { get; protected set; }

        /// <summary>
        /// The current search terms 
        /// </summary>
        [BindProperty]
        public string SearchTerms { get; set; } = "";

        /// <summary>
        /// The filtered MPAA Ratings
        /// </summary>
        [BindProperty]
        public string[] MPAARatings { get; set; }

        /// <summary>
        /// The filtered genres
        /// </summary>
        [BindProperty]
        public string[] Genres { get; set; }

        /// <summary>
        /// The minimum IMDB Rating
        /// </summary>
        [BindProperty]
        public double? IMDBMin { get; set; }

        /// <summary>
        /// The maximum IMDB Rating
        /// </summary>
        [BindProperty]
        public double? IMDBMax { get; set; }

        /// <summary>
        /// The minimum Rotten Tomatoes Rating
        /// </summary>
        [BindProperty]
        public double? RottenMin { get; set; }

        /// <summary>
        /// The maximum Rotten Tomatoes Rating
        /// </summary>
        [BindProperty]
        public double? RottenMax { get; set; }

        public void OnGet(double? IMDBMin, double? IMDBMax, double? RottenMin, double? RottenMax, string SearchTerms, string[] MPAARatings, string[] Genres)
        {
            this.RottenMax = RottenMax;
            this.RottenMin = RottenMin;
            this.IMDBMin = IMDBMin;
            this.IMDBMax = IMDBMax;
            this.MPAARatings = MPAARatings;
            this.Genres = Genres;
            this.SearchTerms = SearchTerms;
            Movies = MovieDatabase.Search(SearchTerms);
            Movies = MovieDatabase.FilterByMPAARating(Movies, MPAARatings);
            Movies = MovieDatabase.FilterByGenre(Movies, Genres);
            Movies = MovieDatabase.FilterByIMDBRating(Movies, IMDBMin, IMDBMax);
            Movies = MovieDatabase.FilterByRottenRating(Movies, RottenMin, RottenMax);
        }
    }
}
