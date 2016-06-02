
namespace _04_KotTakoa
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    // Single Line
    // No COmments
    // Spaces on types

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

        private static List<string> words = new List<string>();
        private static StringBuilder output;

        static void Input()
        {
            var isType = false;

            var lines = int.Parse(Console.ReadLine());

            for (int line = 0; line < lines; line++)
            {
                var input = Console.ReadLine();

                if (input.Contains("//"))
                {
                    input = input.Substring(0, input.LastIndexOf("//"));
                }


                words.AddRange(input
                    .Trim()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList());
            }
        }

        static void Takoa()
        {
            output = new StringBuilder();

            var lastType = "";
            var lastTypeIndex = 0;
            var isComment = false;
            var isStr = false;

            for (int word = 0; word < words.Count; word++)
            {
                var curWord = words[word];

                if (curWord == openComment)
                {
                    isComment = true;
                }
                else if (curWord == closeComment)
                {
                    isComment = false;
                }
                //else if (curWord.Contains("\"") && !isStr)
                //{
                //    isStr = true;
                //}
                //else if (curWord.Contains("\"") && isStr)
                //{
                //    isStr = false;
                //}
                else if (!isComment)
                {
                    output.Append(curWord);

                    foreach (var type in types)
                    {
                        var check = string.Format("{0} ", type);

                        if (curWord.Contains(check))
                        {
                            output.Append(" ");
                        }
                    }

                    if (curWord[curWord.Length - 1] == '>')
                    {
                        output.Append(" ");
                    }

                    if (isStr)
                    {
                        output.Append(" ");
                    }
                }
            }
        }
        static void Main()
        {
            Input();
            Takoa();
            Console.WriteLine(output);
        }
    }
}
