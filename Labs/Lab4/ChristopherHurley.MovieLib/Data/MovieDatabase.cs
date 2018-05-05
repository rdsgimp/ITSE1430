/* Christopher Hurley
 * ITSE 1430
 * Lab 4
 * 4 May, 2018
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristopherHurley.MovieLib.Data
{
    /// <summary>Provides an in-memory Movie database.</summary>
    public abstract class MovieDatabase : IMovieDatabase
    {
        /// <summary>Add a new Movie.</summary>
        /// <param name="Movie">The Movie to add.</param>
        /// <returns>The added Movie.</returns>
        /// <remarks>
        /// Returns an error if Movie is null, invalid or if a Movie
        /// with the same name already exists.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="movie"/> is <see langword="null"/>.</exception>
        /// <exception cref="ValidationException"><paramref name="movie"/> is invalid.</exception>
        /// <exception cref="ArgumentException">A movie with the same title already exists.</exception>
        public Movie Add( Movie Movie )
        {
            //Check for null
            if (Movie == null)
            {
                throw new ArgumentNullException(nameof(Movie));
            };

            //Validate Movie using IValidatableObject
            //var error = Movie.Validate();
            ObjectValidator.Validate(Movie);


            // Verify unique Movie
            var existing = GetMovieByNameCore(Movie.Title);
            if (existing != null)
            {
                throw new ArgumentException("Movie already exists.", nameof(Movie));
            }

            //create new movie
            return AddCore(Movie);
        }

        /// <summary>Gets a specific movie.</summary>
        /// <param name="id">The ID of the movie.</param>
        /// <returns>The movie, if found.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="id"/> is less than or equal to zero.</exception>
        public Movie Get( int id )
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be > 0.");

            return GetCore(id);
        }

        /// <summary>Edits an existing Movie.</summary>
        /// <param name="Movie">The Movie to update.</param>
        /// <param name="message">Error message.</param>
        /// <returns>The updated Movie.</returns>
        /// <remarks>
        /// Returns an error if Movie is null, invalid, Movie name
        /// already exists or if the Movie cannot be found.
        /// </remarks>
        public Movie Update( Movie Movie )
        {
            //message = "";
            //Check for null
            if (Movie == null)
            {
                throw new ArgumentNullException(nameof(Movie));
            };

            //Validate Movie using IValidatableObject
            //var error = Movie.Validate();
            ObjectValidator.Validate(Movie);
            

            // Verify unique Movie
            var existing = GetMovieByNameCore(Movie.Title);
            if (existing != null && existing.Id != Movie.Id)
            {
                throw new ArgumentException("Movie already exists.", nameof(Movie));
            }
            //Find existing
            existing = existing ?? GetCore(Movie.Id);
            if (existing == null)
            {
                throw new ArgumentException("Movie not found.");
            };

            return UpdateCore(Movie);

        }

        /// <summary>Gets all Movies.</summary>
        /// <returns>The list of Movies.</returns>
        public IEnumerable<Movie> GetAll() =>
            //return GetAllCore();
            // Option 1 - LINQ
            from p in GetAllCore()
            orderby p.Title, p.Id descending
            select p;


        /// <summary>Removes a Movie.</summary>
        /// <param name="id">The Movie ID.</param>
        /// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
        /// <remarks>
        /// If the movie does not exist then nothing happens.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="id"/> is less than or equal to zero.</exception>
        public void Remove( int id )
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be > 0.");

            //var existing = GetCore(id);
            //if (existing != null)
            //    return false;

            RemoveCore(id);
            //return true;
        }

        protected abstract Movie AddCore( Movie Movie );
        protected abstract IEnumerable<Movie> GetAllCore();
        protected abstract Movie GetCore( int id );

        protected abstract void RemoveCore( int id );

        protected abstract Movie UpdateCore( Movie Movie );

        protected abstract Movie GetMovieByNameCore( string name );

       // protected abstract string GetTitle( int id );
     


        #region Private Members


        #endregion
    }
}
