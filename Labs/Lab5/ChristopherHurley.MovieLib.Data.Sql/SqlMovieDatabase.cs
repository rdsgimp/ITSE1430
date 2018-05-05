/* Christopher Hurley
 * ITSE 1430
 * Lab 5
 * 4 May, 2018
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;

namespace ChristopherHurley.MovieLib.Data.Sql
{
    /// <summary>Provides an implementation of <see cref="IMovieDatabase"/> using SQL Server.</summary>
    public class SqlMovieDatabase : MovieDatabase
    {
        #region Construction

        public SqlMovieDatabase( string connectionString )
        {
            if (connectionString == null)
                throw new ArgumentNullException(nameof(connectionString));
            if (connectionString == "")
                throw new ArgumentException("Connection string cannot be empty.",
                                            nameof(connectionString));

            //var connString = ConfigurationManager.ConnectionStrings[connectionString];
            _connectionString =  connectionString;
        }
        #endregion

        /// <summary>Adds a movie.</summary>
        /// <param name="movie">The movie to add.</param>
        /// <returns>The added movie.</returns>
        protected override Movie AddCore( Movie movie )
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                //var cmd = conn.CreateStoredProcedureCommand("AddMovie");
                var cmd = new SqlCommand("AddMovie", conn) {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@title", movie.Title);
                cmd.Parameters.AddWithValue("@description", movie.Description);
                cmd.Parameters.AddWithValue("@length", movie.Length);
                //cmd.Parameters.AddWithValue("@isOwned", movie.IsOwned);
                var parm = cmd.CreateParameter();
                parm.ParameterName = "@isOwned";
                parm.DbType = System.Data.DbType.Boolean;
                parm.Value = movie.IsOwned;
                cmd.Parameters.Add(parm);

                conn.Open();
                var result = cmd.ExecuteScalar();

                var id = Convert.ToInt32(result);
                movie.Id = id;
               // movie.Id = cmd.ExecuteScalar<int>();
            };

            return movie;
        }

        /// <summary>Finds a movie by its title.</summary>
        /// <param name="title">The title to find.</param>
        /// <returns>The movie, if any.</returns>
        protected override Movie GetMovieByNameCore( string title )
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("GetAllMovies", conn) {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var movie = ReadData(reader);
                        if (String.Compare(movie.Title, title, true) == 0)
                            return movie;
                    };
                };
            };

            return null;
            //Not supported directly
            //var movies = GetAllCore();
            //return movies.FirstOrDefault(m => String.Compare(m.Title, title, true) == 0);
        }

        /// <summary>Gets a specific movie.</summary>
        /// <param name="id">The ID of the movie.</param>
        /// <returns>The movie, if found.</returns>
        protected override Movie GetCore( int id )
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("GetMovie", conn) {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                //var cmd = conn.CreateStoredProcedureCommand("GetMovie");
                cmd.Parameters.Add(new SqlParameter("@id", id));

                conn.Open();
                //return cmd.ExecuteReaderWithSingleResult(ReadMovie);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return ReadData(reader);
                };
            };
            return null;
        }

        private static Movie ReadData( SqlDataReader reader )
                        => new Movie() {
                            Id = Convert.ToInt32(reader["Id"]),
                            Title = reader.GetFieldValue<string>(1),
                            Description = reader.GetString(2),
                            Length = reader.GetInt32(3),
                            IsOwned = reader.GetBoolean(4)
                        };

        /// <summary>Gets all the movies.</summary>
        /// <returns>The list of movies.</returns>
        protected override IEnumerable<Movie> GetAllCore()
        {
            var items = new List<Movie>();

            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("GetAllMovies", conn) {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                //var cmd = conn.CreateStoredProcedureCommand("GetAllMovies");

                conn.Open();

                var ds = new DataSet();
                var da = new SqlDataAdapter {
                    SelectCommand = cmd
                };

                da.Fill(ds);

                if (ds.Tables.Count == 1)
                {
                    foreach (var row in ds.Tables[0].Rows.OfType<DataRow>())
                    {
                        var movie = new Movie() {
                            Id = Convert.ToInt32(row["Id"]),
                            Title = row.Field<string>("Title"),
                            Description = row.Field<string>("Description"),
                            Length = row.Field<int>("Length"),
                            IsOwned = row.Field<bool>("IsOwned")
                        };

                        items.Add(movie);
                    }
                }

                //return cmd.ExecuteReaderWithResults(ReadMovie);
            };
                return items;
        }

        /// <summary>Removes a movie.</summary>
        /// <param name="id">The ID of the movie.</param>
        protected override void RemoveCore( int id )
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("RemoveMovie", conn) {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@id", id));
               // var cmd = conn.CreateStoredProcedureCommand("RemoveMovie");
                //cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            };
        }

        /// <summary>Updates a movie.</summary>
        /// <param name="movie">The movie to add.</param>
        /// <returns>The updated movie.</returns>
        protected override Movie UpdateCore( Movie movie )
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("UpdateMovie", conn) {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                //var cmd = conn.CreateStoredProcedureCommand("UpdateMovie");
                //cmd.Parameters.AddWithValue("@id", movie.Id);
                cmd.Parameters.Add(new SqlParameter("@id", movie.Id));

                cmd.Parameters.AddWithValue("@title", movie.Title);
                cmd.Parameters.AddWithValue("@description", movie.Description);
                cmd.Parameters.AddWithValue("@length", movie.Length);
                //cmd.Parameters.AddWithValue("@isOwned", movie.IsOwned);
                var parm = cmd.CreateParameter();
                parm.ParameterName = "@IsOwned";
                parm.DbType = System.Data.DbType.Boolean;
                parm.Value = movie.IsOwned;
                cmd.Parameters.Add(parm);

                conn.Open();
                cmd.ExecuteNonQuery();
            };

            return movie;
        }

        #region Private Members

        private Movie ReadMovie( DbDataReader reader ) => new Movie() {
            Id = reader.GetInt32(0),
            Title = reader.GetString(1),
            Description = reader.GetString(2),
            Length = reader.GetInt32(3),
            IsOwned = reader.GetBoolean(4)
        };

        private readonly string _connectionString;
        #endregion
    }
}
