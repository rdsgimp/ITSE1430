/* Christopher Hurley
 * ITSE 1430
 * Lab 5
 * 4 May, 2018
 */

namespace ChristopherHurley.MovieLib.Web.Mvc.Models
{
    public static class MovieViewExtensions
    {
        public static MovieViewModel ToModel( this Movie source )
            => new MovieViewModel() {
                Id = source.Id,
                Title = source.Title,
                Description = source.Description,
                Length = source.Length,
                IsOwned = source.IsOwned
            };

        public static Movie ToDomain( this MovieViewModel source )
             => new Movie() {
                 Id = source.Id,
                 Title = source.Title,
                 Description = source.Description,
                 Length = source.Length,
                 IsOwned = source.IsOwned,
             };
             }
}