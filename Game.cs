/* DT071G - Programmering i C#.NET
 * Project
 * Linn Eriksson, HT23
 */
 
using System;

namespace project
{
    class Game
    {
        public Game ()
        {
            //Konstruktor här
        }

        public bool Menu()
        {
            //Variable for user input.
            int menuInput = 0;

            //The text of the menu.
            Console.Clear();
            Console.WriteLine("Skriv in mer text här.");
            Console.WriteLine("3. Avsluta programmet.");
        
            //Error handling of user-input with try-catch.
            try
            {
                menuInput = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Du gjorde ett ogiltigt menyval.");
            }

            //Switch for the actual menu.
            switch(menuInput)
            {
                case 1:
                    Console.Clear();
                    //Här ska något hända.
                    return true;

                case 2:
                    Console.Clear();
                    //Här ska något annat hända.
                    return true;

                case 3:
                    Console.Clear();
                    return false;

                default:
                    Console.Clear();
                    Console.WriteLine("Du valde ett ogiltigt alternativ, försök igen.");
                    Console.ReadKey();
                    return true;
            }
        }
    }
}
