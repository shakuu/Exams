using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04KOT
{
    class Program
    {
        static string[] types = @"bool, int, char, string, decimal, var, using, class, namespace, static, void, new, struct"
            .Split(new[] { ' ', ',', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .ToArray();

        static string[] oneLiners = @"bool, int, char, string, decimal"
            .Split(new[] { ' ', ',', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .ToArray();

        static string openComment = "/*";
        static string closeComment = "*/";

        static List<string> words = new List<string>();

        static void Input()
        {
            var lines = int.Parse( Console.ReadLine());

            for (int i = 0; i < lines; i++)
            {
                words.Add(Console.ReadLine());
            }
        }

        static void Kot()
        {

            for (int line = 0; line < words.Count; line++)
            {
                for (int chr = 0; chr < words[line].Length; chr++)
                {

                }
            }

        }

        static void Main()
        {
        }
    }
}
