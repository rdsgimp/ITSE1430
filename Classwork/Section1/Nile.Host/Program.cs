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
            
        }

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
