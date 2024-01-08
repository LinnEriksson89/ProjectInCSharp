/* DT071G - Programmering i C#.NET
 * Project
 * Linn Eriksson, HT23
 */
 
using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace project
{
    class Game
    {
        //Variables
        private readonly string fileName = "words.json";
        private List<string> listOfWords;
        private string word;
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
            Console.WriteLine("Hej och välkommen till den här versionen av wordle!");
            Console.WriteLine("Skriv den siffra som motsvarar ditt menyval:");
            Console.WriteLine("1. Spela en omgång.");
            Console.WriteLine("2. Se din statistik.");
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
                    PlayGame();
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

        private void PlayGame()
        {
            //Variables.
            string input, tempString;
            int tempInt, counter = 0, colorCounter = 0, loopCounter = 0, colorInt = 0;
            bool stringContainsInt, writeChar = false;
            char[] inputArray, wordArray;
            char charWord;

            //Error handling with try-catch.
            try
            {
                //Get the word for the game.
                GetWord();
                
                //Information about the game.
                Console.WriteLine("Välkommen till spelet!");
                Console.WriteLine("\nReglerna är enkla, du ska gissa vilket som är omgångens ord.");
                Console.WriteLine("Ordet är på svenska, har fem bokstäver och är ett substantiv.");
                Console.WriteLine("Apostrofer och liknande har städats bort för att förenkla, så istället för é räcker det med att skriva e.");
                Console.WriteLine("När du gissar på ett ord får du svar på om någon av bokstäverna var rätt och om dom var på rätt eller fel plats.");
                Console.WriteLine("Nu börjar vi, vilket ord vill du gissa på?");

                
                    Console.WriteLine(word);

                //Transformation to be able to compare word later.
                word = word.ToLowerInvariant();

            }
            catch
            {
                //If something goes wrong with fetching the word the game is not run.
                Console.WriteLine("Något verkar ha gått fel med ordlistan så spelet kan inte köras.");
                counter = 7;
            }

            
            //While-loop that counts the number of guesses.
            while(counter <= 5)
            {
                //Get input, count the number of letters in it, check if it contains digits.
                input = Console.ReadLine().ToLowerInvariant().Trim();
                tempInt = input.Length;
                stringContainsInt = input.Any(char.IsDigit);
                inputArray = input.ToCharArray();
                wordArray = word.ToCharArray();

                //If numbers are in the word it's not a word and the guess doesn't count.
                if (stringContainsInt)
                {
                    Console.WriteLine("Du verkar ha råkat skriva in några siffror istället för bokstäver.");
                    counter --;
                }
                else if(tempInt == 5)
                {
                    //If the word is 5 characters long we can start the actual game.
                    //Compare the input against the word, first check if it's the correct word.
                    if (word == input)
                    {
                        //Overwrite the inputed word with the word on green background to show all letters are correct.
                        Console.SetCursorPosition(0, Console.CursorTop -1);
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(input);
                        Console.ResetColor();

                        //Give information about winning the game and increse the counter to kill the loop.
                        Console.WriteLine("\nGrattis, " + input + " var rätt ord!");
                        counter = 7;
                    }
                    else
                    {
                        //Compare a lower case version of the word to a lower case version of the input.
                        //Collect the matches and remove those chars, collect the semimatches and remove those chars.
                        var match = input.Select((c, i) => char.ToLowerInvariant(c) == char.ToLowerInvariant(word[i])).ToArray(); 
                        var remaningChars = new string(word.Select((c, i) => match[i] ? '\0' : c).ToArray());
                        var semiMatch = input.Select((c, i) => !match[i] && remaningChars.Contains(c, StringComparison.InvariantCulture)).ToArray();
                        remaningChars = new string(word.Select((c, i) => semiMatch[i] ? '\0' : c).ToArray());
                        
                        StringBuilder result = new();

                        //Set cursor position to write over the input.
                        Console.SetCursorPosition(0, Console.CursorTop -1);

                        //Run through the arrays of letters and write them with the correct background.
                        for (int i=0; i < 5; i++)
                        {
                            if(match[i])
                            {
                                //If the letter is in the correct position, green background.
                                Console.BackgroundColor = ConsoleColor.DarkGreen;
                                Console.Write(inputArray[i]);
                            }
                            else if (semiMatch[i])
                            {
                                //If the letter exists but is in the wrong position, blue background.
                                Console.BackgroundColor = ConsoleColor.DarkBlue;
                                Console.Write(inputArray[i]);
                            }
                            else
                            {
                                //If the letter does not exist in the word, red background.
                                Console.BackgroundColor = ConsoleColor.DarkRed;
                                Console.Write(inputArray[i]);
                            }
                        }

                        //Make sure the new input is on a new line and the color is reset.
                        Console.ResetColor();
                        Console.Write("\n");
                    }

                }
                else
                {
                    //If the word is not five letters the guess doesn't count.
                    Console.WriteLine("Du verkar ha gissat på ett ord som är för långt eller för kort, försök igen!");
                    Console.WriteLine("Kom ihåg att ordet ska vara 5 bokstäver långt, din gissning (" + input + ") är på " + tempInt + " bokstäver.");
                    counter--;
                }

                counter++;
            }

            //Pauses and then returns to menu.
            ReturnToMenu();
        }

        private string GetWord()
        {
            //Variables.
            int tempInt, index;
            Random randomizer = new Random();

            //If the file that contains the words exists this part runs.
            if(File.Exists(fileName))
            {
                //Try-catch to read the file and turn the json into a list.
                try
                {
                    using(StreamReader streamReader = new StreamReader(fileName))
                    {
                        string json = streamReader.ReadToEnd();
                        listOfWords = JsonSerializer.Deserialize<List<string>>(json);
                        streamReader.Close();

                        
                        //If the list exists.
                        if(listOfWords != null)
                        {
                            //Get the length of the list, use it to randomize a number and use that to fetch a word from the list.
                            tempInt = listOfWords.Count;
                            index = randomizer.Next(0, tempInt);
                            word = listOfWords[index];

                            return word;
                        }
                    }

                }
                catch
                {
                    //If the reading fails.
                    Console.WriteLine("Något gick fel när ordet skulle läsas in, spelet kommer därför alltid att använda samma ord.");
                }
            }
            else
            {
                //If the list of words have broken for some reason.
                Console.WriteLine("Filen med ord verkar saknas, spelet kommer därför alltid använda samma ord!");
            }

            //As a sort of easter egg the game will run on the word "error" if something goes wrong with the file of words.
            return "error";
        }
        private static void ReturnToMenu()
        {
            //So the program pauses before returning to menu.
            Console.WriteLine("\nTryck på valfri knapp för att återvända till menyn.");
            Console.ReadKey();
        }
    }
}
