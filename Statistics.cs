/* DT071G - Programmering i C#.NET
 * Project
 * Linn Eriksson, HT23
 */

using System;

namespace project
{
    class Statistics
    {
        public Statistics(int turns, string word, bool winner)
        {
            //Constructor of statistics.
            Word = word;
            Turns = turns;
            Winner = winner;
            Time = DateTime.Now;
        }

        public string Word { get; set; }
        public int Turns { get; set; }
        public bool Winner { get; set; }
        public DateTime Time { get; set; }
    }
}