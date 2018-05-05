/* Christopher Hurley
 * ITSE 1430
 * Lab 5
 * 4 May, 2018
 */
using System;
using System.ComponentModel.DataAnnotations;


namespace ChristopherHurley.MovieLib.Web.Mvc.Models
{
    public class MovieViewModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = "Length must be >= 0")]
        public int Length { get; set; }

        public bool IsOwned { get; set; }

    }
}