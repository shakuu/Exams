using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05_Flights
{
    class Program
    {
        private static bool[,] input;
        private static bool[,] flags;

        static void Input()
        {
            var lines = int.Parse(Console.ReadLine());

            input = new bool[lines, lines];
            flags = new bool[lines, lines];

            for (int i = 0; i < lines; i++)
            {
                var line = Console.ReadLine().Trim()
                    .Split(' ')
                    .Select(int.Parse)
                    .ToArray();

                input[line[0], line[1]] = true;
                input[line[1], line[0]] = true;
            }
        }

        static void Main()
        {
            Input();

            for (int row = 0; row < input.GetLength(0); row++)
            {
                for (int col = 0; col < input.GetLength(1); col++)
                {
                    if (!flags[row, col] && input[row, col])
                    {
                        SearchForFlights(col);
                        flags[row, col] = true;
                    }
                }
            }
        }

        static void SearchForFlights(int nextRow)
        {

        }
    }
}
