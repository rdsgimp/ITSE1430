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
        private string _name = "";
        private string _description;
        //private decimal _price;
        //bool _isDiscontinued;

        public string Name
        {
            get { return _name ?? ""; }
            set { _name = value ?? ""; }
        }
        public string Description { get => _description ?? ""; set => _description = value ?? ""; }

        // using auto property here // and set to 0
        public decimal Price { get; set; } = 0;


    //    public int ShowingOffAccessibility
    //    {   // getter or setter must always be more restrictive in mixed permissions
   //       get { }
    //        internal set { }
    //    }

        /// <summary>Gets the price, with discount, if any.</summary>
        public decimal ActualPrice
        {
            get
            {
                if (IsDiscontinued)
                    return Price - (Price * DiscountPercentage);

                return Price;
            }
            //set { }
        }
        public bool IsDiscontinued { get; set; }

        public string Validate()
        {
            // name is required
            if (String.IsNullOrEmpty(_name))
                return "Name cannot be empty";

            // price >= 0
            if (Price < 0)
                return "Price must be >= 0";

            return "";
        }
    }
}


/// <summary>Get the product name.</summary>
/// <returns>The Name</returns>
// public string GetName() => _name ?? "";


/// <summary>Sets the product name.</summary>
/// <param name="value">The name.</param>
// public void SetName( string value ) => _name = value ?? "";

/// <summary>Validates the product.</summary>
/// <returns>Error message, if any.</returns>
