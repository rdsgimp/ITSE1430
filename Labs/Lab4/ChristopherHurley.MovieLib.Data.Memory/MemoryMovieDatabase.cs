/* Christopher Hurley
 * ITSE 1430
 * Lab 4
 * 4 May, 2018
 */
using System;
using System.Collections.Generic;

namespace ChristopherHurley.MovieLib.Data.Memory
{
    /// <summary>Provides an in-memory Movie database.</summary>
    public class MemoryMovieDatabase : MovieDatabase
    {
        /// <summary>Initializes an instance of the <see cref="MemoryMovieDatabase"/> class.</summary>
        public MemoryMovieDatabase()
        {
            /// Pre load some movie data
            //_movies = new List<Movie>()
            //{
            //    new Movie() { Id = _nextId++, Title = "A Sample Movie",
            //                    IsOwned = true, Length = 90, },
            //    new Movie() { Id = _nextId++, Title = "That one movie",
            //                    IsOwned = true, Length = 81, Description = "The one with that guy from that other movie" },
            //    new Movie() { Id = _nextId++, Title = "That other movie",
            //                    IsOwned = false, Length = 88, Description = "Has that one guy, with the face, you know the one." }
            //};
            
        }

        /// <summary>Add a new Movie.</summary>
        /// <param Title="Movie">The Movie to add.</param>
        /// <param Title="message">Error message.</param>
        /// <returns>The added Movie.</returns>
        /// <remarks>
        /// Returns an error if Movie is null, invalid or if a Movie
        /// with the same Title already exists.
        /// </remarks>
        protected override Movie AddCore( Movie Movie )
        {
            // Clone the object
            Movie.Id = _nextId++;
            _movies.Add(Clone(Movie));

            // Return a copy
            return Movie;
        }

        /// <summary>Edits an existing Movie.</summary>
        /// <param Title="Movie">The Movie to update.</param>
        /// <param Title="message">Error message.</param>
        /// <returns>The updated Movie.</returns>
        /// <remarks>
        /// Returns an error if Movie is null, invalid, Movie Title
        /// already exists or if the Movie cannot be found.
        /// </remarks>
        protected override Movie UpdateCore( Movie Movie )
        {
            var existing = GetCore(Movie.Id);

            // Clone the object
            Copy(existing, Movie);

            //Return a copy
            return Movie;
        }

        /// <summary>Gets all Movies.</summary>
        /// <returns>The list (a copy) of Movies.</returns>
        protected override IEnumerable<Movie> GetAllCore()
        {
            // iterator syntax
            foreach (var Movie in _movies)
            {
                if (Movie != null)
                    yield return Clone(Movie);
            }
        }

        /// <summary>Removes a Movie.</summary>
        /// <param Title="id">The Movie ID.</param>
        protected override void RemoveCore( int id )
        {
            var existing = GetCore(id);
            if (existing != null)
                _movies.Remove(existing);
        }

        #region Private Members

        //Clone a Movie
        private Movie Clone( Movie item )
        {
            var newMovie = new Movie();
            Copy(newMovie, item);

            return newMovie;
        }

        //Copy a Movie from one object to another
        private void Copy( Movie target, Movie source )
        {
            target.Id = source.Id;
            target.Title = source.Title;
            target.Description = source.Description;
            target.Length = source.Length;
            target.IsOwned = source.IsOwned;
        }


        //Find a Movie by its name
        protected override Movie GetMovieByNameCore( string Title )
        {
            //for (var index = 0; index < _Movies.Length; ++index)
            foreach (var Movie in _movies)
            {
                // Movie.Title.CompareTo();
                if (String.Compare(Movie.Title, Title, true) == 0)
                    return Movie;
            };

            return null;
        }
        protected override Movie GetCore( int id )
        {
            //for (var index = 0; index < _Movies.Length; ++index)
            foreach (var Movie in _movies)
            {
                if (Movie.Id == id)
                    return Movie;
            };

            return null;
        }

        protected string GetTitle( int id )
        {
            //for (var index = 0; index < _Movies.Length; ++index)
            foreach (var Movie in _movies)
            {
                if (Movie.Id == id)
                    return Movie.Title;
            };

            return null;
        }

        private readonly List<Movie> _movies = new List<Movie>();
        private int _nextId = 1;

        #endregion
    }
}
