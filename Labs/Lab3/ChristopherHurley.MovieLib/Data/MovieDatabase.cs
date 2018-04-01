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
        /// <param name="message">Error message.</param>
        /// <returns>The added Movie.</returns>
        /// <remarks>
        /// Returns an error if Movie is null, invalid or if a Movie
        /// with the same name already exists.
        /// </remarks>
        public Movie Add( Movie Movie, out string message )
        {
            //Check for null
            if (Movie == null)
            {
                message = "Movie cannot be null.";
                return null;
            };

            //Validate Movie using IValidatableObject
            //var error = Movie.Validate();
            var errors = ObjectValidator.Validate(Movie);
            if (errors.Count() > 0)
            {
                //Get first error
                message = errors.ElementAt(0).ErrorMessage;
                return null;
            };

            // Verify unique Movie
            var existing = GetMovieByNameCore(Movie.Title);
            if (existing != null)
            {
                message = "Movie already exists.";
                return null;
            }

            message = null;
            return AddCore(Movie);

        }

        /// <summary>Edits an existing Movie.</summary>
        /// <param name="Movie">The Movie to update.</param>
        /// <param name="message">Error message.</param>
        /// <returns>The updated Movie.</returns>
        /// <remarks>
        /// Returns an error if Movie is null, invalid, Movie name
        /// already exists or if the Movie cannot be found.
        /// </remarks>
        public Movie Update( Movie Movie, out string message )
        {
            message = "";
            //Check for null
            if (Movie == null)
            {
                message = "Movie cannot be null.";
                return null;
            };

            //Validate Movie using IValidatableObject
            //var error = Movie.Validate();
            var errors = ObjectValidator.Validate(Movie);
            if (errors.Count() > 0)
            {
                //Get first error
                message = errors.ElementAt(0).ErrorMessage;
                return null;
            };

            // Verify unique Movie
            var existing = GetMovieByNameCore(Movie.Title);
            if (existing != null && existing.Id != Movie.Id)
            {
                message = "Movie already exists.";
                return null;
            }
            //Find existing
            existing = existing ?? GetCore(Movie.Id);
            if (existing == null)
            {
                message = "Movie not found.";
                return null;
            };

            return UpdateCore(Movie);

        }

        /// <summary>Gets all Movies.</summary>
        /// <returns>The list of Movies.</returns>
        public IEnumerable<Movie> GetAll()
        {
            return GetAllCore();
        }
        //public IEnumerable<Movie> GetAll()
        //{
        //    //Return a copy so caller cannot change the underlying data
        //    var items = new List<Movie>();

        //    //for (var index = 0; index < _Movies.Length; ++index)
        //    foreach (var Movie in _Movies)
        //    {
        //        if (Movie != null)
        //            items.Add(Clone(Movie));
        //    };

        //    return items;
        //}

        /// <summary>Removes a Movie.</summary>
        /// <param name="id">The Movie ID.</param>
        public void Remove( int id )
        {
            //TODO: Return an error if id <= 0

            if (id > 0)
            {
                RemoveCore(id);
            };
        }

        protected abstract Movie AddCore( Movie Movie );
        protected abstract IEnumerable<Movie> GetAllCore();
        protected abstract Movie GetCore( int id );

        protected abstract void RemoveCore( int id );

        protected abstract Movie UpdateCore( Movie Movie );

        protected abstract Movie GetMovieByNameCore( string name );

       // protected abstract string GetTitle( int id );
     


        #region Private Members

        //private int FindEmptyMovieIndex()
        //{
        //    for (var index = 0; index < _Movies.Length; ++index)
        //    {
        //        if (_Movies[index] == null)
        //            return index;
        //    };

        //    return -1;
        //}

        //Find a Movie by its ID


        #endregion
    }
}
