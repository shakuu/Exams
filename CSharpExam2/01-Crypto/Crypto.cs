

namespace _01_Crypto
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Numerics;

    class Crypto
    {
        private static string base26;
        private static string base7;
        private static string operation;

        static string alphabet = "abcdefghijklmnopqrstuvwxyz";
        private static BigInteger result;

        static void Input()
        {
            base26 = Console.ReadLine();

            operation = Console.ReadLine();

            base7 = Console.ReadLine();
        }

        static BigInteger Input26ToDec(string number)
        {
            BigInteger sum = 0;

            foreach (var ltr in number)
            {
                var digit = alphabet.IndexOf(ltr);
                sum = digit + sum * 26;
            }

            return sum;
        }

        static BigInteger Input7ToDec(string number)
        {
            BigInteger sum = 0;

            foreach (var ltr in number)
            {
                var digit = ltr - '0';
                sum = digit + sum * 7;
            }

            return sum;
        }

        static void DecToBase9(BigInteger number)
        {
            var output = new StringBuilder();

            if (number == 0)
            {
                Console.WriteLine("0");
                return;
            }

            while (number > 0)
            {
                var digit = number % 9;
                number /= 9;

                output.Insert(0, digit);
            }

            Console.WriteLine(output);
        }

        static void Main()
        {
            Input();
            var number1 = Input26ToDec(base26);
            var number2 = Input7ToDec(base7);
            result = 0;

            if (operation == "-")
            {
                result = number1 - number2;
            }
            else
            {
                result=  number1 + number2;
            }

            DecToBase9(result);
        }
    }
}
