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
        Movie Update( Movie movie, out string message );
        IEnumerable<Movie> GetAll();
        void Remove( int id );
        //string GetTitle(int id);
    }
}
