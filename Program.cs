/* DT071G - Programmering i C#.NET
 * Project
 * Linn Eriksson, HT23
 */

using System;

namespace project
{
    class Program
    {
        static void Main(string[] args)
        {
            //Variable and object created.
            Game game = new Game();
            bool showMenu = true;

            while(showMenu)
            {
                showMenu = game.Menu();
            }

            //End of program.
            Console.WriteLine("Tryck på valfri tangent för att avsluta programmet.");
            Console.ReadKey();
        }
    }
}
