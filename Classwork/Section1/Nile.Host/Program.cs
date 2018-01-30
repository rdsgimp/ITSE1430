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
                switch (choice)
                {
                    case 'l':
                    case 'L': ListProducts(); break;
                    case 'a':
                    case 'A': AddProduct(); break;
                    case 'q':
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
                Console.WriteLine("Value must be greater than or equal to: {0}", minValue);
                //Console.WriteLine("Value must be greater than or equal to: " + minValue);
                Console.WriteLine("geet this here" + result);
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

                string input = Console.ReadLine();

                if (input == "L" || input == "l")
                {
                    return input[0];
                } else if (input == "A" || input == "a" )
                {
                    return input[0];
                } else if (input == "Q" || input == "q")
                {
                    return input[0];
                }

                Console.WriteLine("Please choose a valid option");
            } while (true);

        }

        private static void ListProducts()
        {
            // are there any prod
            if (_name != null && _name != "")
            {
                // display a product
                Console.WriteLine(_name);
                Console.WriteLine(_price);
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
    }

}
