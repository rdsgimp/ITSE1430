using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using dt = System.DateTime;

namespace Nile.Host
{
    class Program
    {
        static void Main( string[] args )
        {
            bool quit = false;
            while (!quit)
            {
                // display menu
                char choice = DisplayMenu();
                // process menu selection    
                switch (Char.ToUpperInvariant(choice))
                {
                    //case 'l':
                    case 'L': ListProducts(); break;
                    //case 'a':
                    case 'A': AddProduct(); break;
                    //case 'q':
                    case 'Q': quit = true; break;
                };
            };
        }

        static void AddProduct()
        {
            // get name
             _name = ReadString("Enter name: ", true);

            // get price
             _price = ReadDecimal("Enter price: ", 0);

            // get desc
             _description = ReadString("Enter optional description: ", false);
        }

        private static decimal ReadDecimal( string message, decimal minValue )
        {
            do
            {
                Console.Write(message);

                string value = Console.ReadLine();

                //decimal result;

                if (Decimal.TryParse(value, out decimal result))
                {

                    // if not required or not empty
                    if (result >= minValue || value != null)
                        return result;
                }
                string msg = String.Format("Value must be greater than or equal to: {0}", minValue);
                Console.WriteLine(msg);
                //Console.WriteLine("Value must be greater than or equal to: {0}", minValue);
                //Console.WriteLine("Value must be greater than or equal to: " + minValue);
            } while (true);
            // out param result is now out of scope
            // Console.WriteLine("geet this here" + result);

        }

        private static string ReadString(string message, bool isRequired)
        {
            do
            {
                Console.Write(message);

                string value = Console.ReadLine();

                // if not required or not empty
                if (!isRequired || value != "")
                    return value;
                Console.WriteLine("Value is required");
            } while (true);
        }

        private static char DisplayMenu()
        {

            do
            {

                Console.WriteLine("L)ist Products");
                Console.WriteLine("A)dd Product");
                Console.WriteLine("Q)uit");
 

                string input = Console.ReadLine().ToUpperInvariant();

                // trim whitespace
                // input = input.Trim();

                //padding
                //input = input.PadLeft(10);

                // starts with
                //input.StartsWith(@"\");
                //input.EndsWith(@"\");

                //substr
                // string newValue = input.Substring(0, 10);



                //if (input == "L" )
                // case insensitive comparison via 3rd param in .Compare
                if (String.Compare(input, "L", true) == 0)
                {
                    return input[0];
                } else if (input == "A" )
                {
                    return input[0];
                } else if (input == "Q" )
                {
                    return input[0];
                }

                Console.WriteLine("Please choose a valid option");
            } while (true);
            //Console.WriteLine()

        }

        private static void ListProducts()
        {
            //_name.
            // are there any prod
            //if (_name != null && _name != "")
            if (!String.IsNullOrEmpty(_name))
            {
                // display a product: Name [$price]
                //                    <description>

                // String formatting
                //var msg = String.Format("{0} [${1}]", _name, _price);

                // String concat
                //var msg = _name + " [$" + _price + "]";

                //String concat 2
                //var msg = String.Concat(_name, " [$", _price, "]");
                //StringBuilder builder = new StringBuilder(  );
                //builder.

                // String interpolation
                string msg = $"{_name} [${_price}]";
                Console.WriteLine(msg);


                //Console.WriteLine(_name);
                //Console.WriteLine(_price);

                if(!String.IsNullOrEmpty(_description))
                Console.WriteLine(_description);
            } else
                Console.WriteLine("No Products");
        }

        // data for a product
        static string _name;
        static decimal _price;
        static string _description;





        static void PlayingWithPrimitives()
        {
            //Primitive
            var unitPrice = 10.5m;

            //real decl
            System.Decimal unitPrice2 = 10.66m;

            //Current thyme
            dt dateTime = dt.Now;

            System.Collections.ArrayList ayyy;
        }

        static void PlayingWithVariables()
        {
            //single decl
            int hours = 0;
            double rate = 10.25;


            //still not assigned
            //if (false)
            //    hours = 0;

            int hours2 = hours;

            //multi decl
            string firstName, lastName;

            //string @class;


            firstName = "Bob";
            lastName = "Miller";

            firstName = lastName = "Sue";

            //math ops
            int x = 0, y = 10;
            int add = x + y;
            int subtract = x - y;
            int multiply = x * y;
            int divide = x / y;
            int modulus = x % y;

            // x = x + 10
            x += 10;
            double ceiling = Math.Ceiling(rate);
            double floor = ceiling;
        }

        static void PlayingWithReferences()
        {
            // compiler changes code to new string() under the hood
            var message = "Hello";

            string name = null;
            name = new string('*', 10);

            object instance = message;
            //String.Format(instance);

            // is operator (for rare value types)
            if (instance is string)
            {
                string str = (string)instance;
                Console.WriteLine(str);
            } else
                Console.WriteLine("Not a string");

            // as operator (only works with reference types)
            string str2 = instance as string;
            if (str2 != null)
            {
                Console.WriteLine(str2);
            } else
                Console.WriteLine("Not a string");

            //pattern matching
            if (instance is string str3)
            {
                Console.WriteLine(str3);
            } else
                Console.WriteLine("Not a string");



            if (name != null)
                name = name + "";
            else
                name = "";

            // c# coalesce takes first non null value
            string name2 = name ?? " ";

            // ? if null, doesn't continue evaluating to the right
            name2 = name2?.PadLeft(10).TrimEnd();

            //name2.PadLeft(10).TrimEnd();



        }

       

        
    }
}
