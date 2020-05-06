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
            Movies = MovieDatabase.All;
            // Search movie titles for the SearchTerms
            if (SearchTerms != null)
            {
                Movies = from movie in Movies
                         where movie.Title != null && movie.Title.Contains(SearchTerms, StringComparison.InvariantCultureIgnoreCase)
                         select movie;
            }
            if (MPAARatings != null && MPAARatings.Length != 0)
            {
                Movies = Movies.Where(movie =>
                    movie.MPAARating != null &&
                    MPAARatings.Contains(movie.MPAARating)
                    );
            }
            if (Genres != null && Genres.Length != 0)
            {
                Movies = Movies.Where(movie =>
                    movie.MajorGenre != null &&
                    Genres.Contains(movie.MajorGenre)
                    );
            }
            if (IMDBMax != null)
            {
                Movies = Movies.Where(movie =>
                    movie.IMDBRating != null &&
                    movie.IMDBRating <= IMDBMax
                    );
            }
            if (IMDBMin != null)
            {
                Movies = Movies.Where(movie =>
                    movie.IMDBRating != null &&
                    IMDBMin <= movie.IMDBRating
                    );
            }
            if (RottenMax != null)
            {
                Movies = Movies.Where(movie =>
                    movie.RottenTomatoesRating != null &&
                    movie.RottenTomatoesRating <= RottenMax
                    );
            }
            if (RottenMin != null)
            {
                Movies = Movies.Where(movie =>
                    movie.RottenTomatoesRating != null &&
                    RottenMin <= movie.RottenTomatoesRating
                    );
            }
        }
    }
}
