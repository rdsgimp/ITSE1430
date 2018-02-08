using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile
{
    /// <summary>Provides information about a product.</summary>
   public class Product
    {
        internal decimal DiscountPercentage = 0.10M;

        /// <summary>Get the product name.</summary>
        /// <returns>The Name</returns>
        public string GetName() => _name ?? "";
  

        /// <summary>Sets the product name.</summary>
        /// <param name="value">The name.</param>
        public void SetName( string value ) => _name = value ?? "";

        /// <summary>Validates the product.</summary>
        /// <returns>Error message, if any.</returns>
        public string Validate()
        {
            // name is required
            if (String.IsNullOrEmpty(_name))
                return "Name cannot be empty";

            // price >= 0
            if (_price < 0)
                return "Price must be >= 0";

            return "";
        }

        string _name = "";     
        string _description;  
        decimal _price;       
        bool _isDiscontinued;

        public string Description { get => _description; set => _description = value; }
        public decimal Price { get => _price; set => _price = value; }
    }
}
