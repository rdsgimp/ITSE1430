/* Christopher Hurley
 * ITSE 1430
 * Lab 1
 * 7 Feb, 2018
 */
using System;

namespace ChristopherHurley.MovieLib.Host
{
    class Program
    {
        // data for a movie
        static string s_name;
        static string s_description;
        static int s_length;
        static bool s_owned;


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
                   
                    case 'L':
                    ListMovies();
                    break;
                    case 'A':
                    AddMovie();
                    break;
                    case 'R':
                    RemoveMovie();
                    break;
                    case 'Q':
                    quit = true;
                    break;
                };
            };
        }

        static void AddMovie()
        {
            // get title
            s_name = ReadString("Enter movie title: ", true);

            // get desc
            s_description = ReadString("Enter description (optional): ", false);

            // get length
            s_length = ReadInt("Enter length in minutes (optional): ", 0, false);

            // get owned T/F
            s_owned = ReadBool("Do you own this movie? (Y/N): ", true);

        }

        private static int ReadInt( string message, int minValue, bool isRequired )
        {
            do
            {
                Console.Write(message);
                string value = Console.ReadLine();

                // default to zero if optional and no value entered
                if (!isRequired && value == "")
                    return 0;

                if (Int32.TryParse(value, out int result))
                {
                    // if >= 0
                    if (result >= minValue)
                        return result;
                }
                string msg = String.Format("Value must be greater than or equal to: {0}", minValue);
                Console.WriteLine(msg);
            } while (true);
            // out param result is now out of scope
            // Console.WriteLine("geet this here" + result);
        }

        private static string ReadString( string message, bool isRequired )
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

        private static bool ReadBool( string message, bool isRequired )
        {
            do
            {
                Console.Write(message);

                string value = Console.ReadLine();

                if (value == "y" || value == "Y")
                    return true;
                if (value == "n" || value == "N")
                    return false;
                Console.WriteLine("Value is required (Y/N)");
            } while (true);
        }

        private static char DisplayMenu()
        {
            do
            {
                Console.WriteLine("\nL)ist Movies");
                Console.WriteLine("A)dd Movie");
                Console.WriteLine("R)emove Movie");
                Console.WriteLine("Q)uit\n");

                string input = Console.ReadLine().ToUpperInvariant();

                if (input == "L")
                {
                    return input[0];
                }
                else if (input == "A")
                {
                    return input[0];
                }
                else if (input == "R")
                {
                    return input[0];
                }
                else if (input == "Q")
                {
                    return input[0];
                }
                Console.WriteLine("Please choose a valid option");
            } while (true);

        }

        private static void ListMovies()
        {
            if (!String.IsNullOrEmpty(s_name))
            {
                // String interpolation
                string msg = $"\nMovie title: {s_name}"; 
                Console.WriteLine(msg);

                if (!String.IsNullOrEmpty(s_description))
                    Console.WriteLine("Description: " +s_description);
                if (s_length > 0)
                    Console.WriteLine("Runtime:     " + s_length + " minutes");
                Console.WriteLine("Owned?:      " + s_owned);
            } else
                Console.WriteLine("\nNo Movies available\n");
        }

        private static void RemoveMovie()
        {
            // might as well reuse ReadBool() since we made it
            bool selection = ReadBool("Are you sure you want to delete the movie? (Y/N)?", true);
            if (selection) // simply remove values since code displays "No movies" if s_name is empty string
            {
                s_name = "";
                s_description = "";
                s_length = 0;
                s_owned = false;
                Console.WriteLine("Movie deleted");
            } else // do nothing and return
                return;
        }

    }
}
