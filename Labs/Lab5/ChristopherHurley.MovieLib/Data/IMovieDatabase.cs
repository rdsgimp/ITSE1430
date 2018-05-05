/* Christopher Hurley
 * ITSE 1430
 * Lab 5
 * 4 May, 2018
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
        /// <returns>The newly added movie if it is added successfully.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="movie"/> is <see langword="null"/>.</exception>
        /// <exception cref="ValidationException"><paramref name="movie"/> is invalid.</exception>
        /// <exception cref="ArgumentException">A movie with the same title already exists.</exception>
        Movie Add( Movie movie );

        /// <summary>
        /// Updates a movie in the database.
        /// </summary>
        /// <param name="movie">Updated movie object</param>
        /// <returns>The updated movie.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="movie"/> is <see langword="null"/>.</exception>
        /// <exception cref="ValidationException"><paramref name="movie"/> is invalid.</exception>
        /// <exception cref="ArgumentException">
        /// A movie with the same title already exists.
        /// <para>-or-</para>
        /// The movie does not exist.
        /// </exception>        
        Movie Update( Movie movie );

        /// <summary>Gets a specific movie.</summary>
        /// <param name="id">The ID of the movie.</param>
        /// <returns>The movie, if found.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="id"/> is less than or equal to zero.</exception>
        Movie Get( int id );

        /// <summary>
        /// Gets all the movies from the database.
        /// </summary>
        /// <returns>An enumerable list of movies.</returns>
        IEnumerable<Movie> GetAll();

        /// <summary>
        /// Removes the specified movie from the database. If the movie does not exist then nothing happens
        /// </summary>
        /// <param name="id">The unique ID of the movie</param>
        /// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
        /// <remarks>
        /// If the movie does not exist then nothing happens.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="id"/> is less than or equal to zero.</exception>
        void Remove( int id );

        // a failed, orphaned attempt to shoehorn my old error message prompt w/ movie title, abandoned for time
        //string GetTitle(int id);
    }
}
