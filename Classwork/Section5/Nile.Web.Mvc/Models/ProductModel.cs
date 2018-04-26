/*
 * ITSE 1430
 * Classwork
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nile.Web.Mvc.Models
{
    /// <summary>Provides information about a product.</summary>
    public class ProductModel
    {   
        /// <summary>Gets or sets the product ID.</summary>
        public int Id { get; set; }

        /// <summary>Gets or sets the description.</summary>
        public string Description { get; set; }

        /// <summary>Gets or sets the name.</summary>
        //[RequiredAttribute]
        [Required(AllowEmptyStrings = false)]     
        //[StringLength(1)]
        public string Name { get; set; }
        
        /// <summary>Gets or sets the price.</summary>
        [Range(0, Double.MaxValue, ErrorMessage = "Price must be >= 0")]
        public decimal Price { get; set; }

        /// <summary>Determines if the product is discontinued.</summary>
        public bool IsDiscontinued { get; set; }
    }
}
