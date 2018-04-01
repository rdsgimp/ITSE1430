/* Christopher Hurley
 * ITSE 1430
 * Lab 3
 * 1 April, 2018
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristopherHurley.MovieLib.Data
{
    /// <summary>
    /// Interface spec for MovieDatabase
    /// </summary>
    public interface IMovieDatabase
    {
        /// <summary>
        /// Adds a movie to the database
        /// </summary>
        /// <param name="movie">The movie to add to the database</param>
        /// <param name="message">reports the error message, if any</param>
        /// <returns>The newly added movie if it is added successfully.</returns>
        Movie Add( Movie movie, out string message );

        /// <summary>
        /// Updates a movie in the database.
        /// </summary>
        /// <param name="movie">Updated movie object</param>
        /// <param name="message">error message, if any</param>
        /// <returns>The updated movie.</returns>
        Movie Update( Movie movie, out string message );

        /// <summary>
        /// Gets all the movies from the database.
        /// </summary>
        /// <returns>An enumerable list of movies.</returns>
        IEnumerable<Movie> GetAll();

        /// <summary>
        /// Removes the specified movie from the database. If the movie does not exist then nothing happens
        /// </summary>
        /// <param name="id">The unique ID of the movie</param>
        void Remove( int id );

        // a failed, orphaned attempt to shoehorn my old error message prompt w/ movie title, abandoned for time
        //string GetTitle(int id);
    }
}
