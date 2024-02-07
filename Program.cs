/* DT071G - Programmering i C#.NET
 * Project
 * Linn Eriksson, HT23
 */

using System.Text;

namespace project
{
    class Program
    {
        static void Main(string[] args)
        {
            //For some reason some consoles does not understand how char is supposed to work in C# and this is needed.
            Console.InputEncoding = Encoding.Unicode;

            //Variable and object created.
            Game game = new Game();
            bool showMenu = true;

            while (showMenu)
            {
                showMenu = game.Menu();
            }

            //End of program.
            Console.WriteLine("Tryck på valfri tangent för att avsluta programmet.");
            Console.ReadKey();
        }
    }
}