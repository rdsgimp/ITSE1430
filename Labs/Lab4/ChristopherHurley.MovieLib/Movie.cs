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
using System.ComponentModel.DataAnnotations;


namespace ChristopherHurley.MovieLib
{
    /// <summary>
    /// Our movie class, consisting of: Title, Desc, length, status of ownership.
    /// Title is a required field, others are optional, however length must be >=0 if provided
    /// </summary>
    public class Movie : IValidatableObject
    {
        private string _title = "";
        private string _description;

        /// <summary>
        /// The title of the movie. (Required)
        /// </summary>
        public string Title
        {
            get => _title ?? "";
            set => _title = value ?? "";
        }
        /// <summary>
        /// Movie description (optional)
        /// </summary>
        public string Description { get => _description ?? ""; set => _description = value ?? ""; }

        // using auto property here // and set to 0
        /// <summary>
        /// Movie length, in minues
        /// </summary>
        public int Length { get; set; } = 0;

        /// <summary>
        /// Set to true if the movie is owned, otherwise false.
        /// </summary>
        public bool IsOwned { get; set; }
        /// <summary>
        /// used to uniquely id records
        /// </summary>
        public int Id { get;  set; }

        /// <summary>
        /// Validate the required title exists, and length is >=0 if provided
        /// </summary>
        /// <returns>Error description if title is empty or null, or length < 0, otherwise returns an empty string</returns>
        public IEnumerable<ValidationResult> Validate( ValidationContext validationContext )
        {
            var errors = new List<ValidationResult>();

            // name is required
            if (String.IsNullOrEmpty(_title))
                errors.Add(new ValidationResult("Name cannot be empty", new[] { nameof(Title) }));


            // length >= 0
            if (Length < 0)
                errors.Add(new ValidationResult("Length must be >= 0", new[] { nameof(Length) }));
            //return "Length must be >= 0";

            return errors;
        }
    }
}
