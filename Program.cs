using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var numbers = ReadNumbersFromConsole();
            if (numbers == null || numbers.Count == 0)
            {
                Console.WriteLine("Don't have data number");
                return;
            }

            PrintResults(numbers);

            Console.WriteLine();
            Console.WriteLine(" Enter to exit...");
            Console.ReadLine();
        }

        private static List<double> ReadNumbersFromConsole()
        {
            while (true)
            {
                Console.WriteLine(" put number ( press space bar ) Enter: ");
                var line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                {
                    Console.WriteLine("don't have data pleas do agin or Ctrl+C to exit");
                    continue;
                }

                var tokens = line.Split(new[] { ' ', ',', ';', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                var numbers = new List<double>();
                var success = true;

                foreach (var t in tokens)
                {
                    double v;

                    if (!double.TryParse(t, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out v)
                        && !double.TryParse(t, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out v))
                    {
                        Console.WriteLine($" Can't '{t}' number ");
                        success = false;
                        break;
                    }

                    numbers.Add(v);
                }

                if (success)
                {
                    return numbers;
                }

                Console.WriteLine(" pleas input data agin ");
            }
        }

        private static void PrintResults(List<double> numbers)
        {
            numbers.Sort();
            var ascending = numbers.ToList();
            var descending = numbers.AsEnumerable().Reverse().ToList();

            var average = numbers.Average();
            var min = numbers.First();
            var max = numbers.Last();
            var median = ComputeMedian(numbers);

            Console.WriteLine();
            Console.WriteLine("Calculation results:");
            Console.WriteLine($"average      : {average}");
            Console.WriteLine($"max         : {max}");
            Console.WriteLine($"min        : {min}");
            Console.WriteLine($"median     : {median}");
            Console.WriteLine();
            Console.WriteLine("Arranged from highest to lowest:");
            Console.WriteLine(string.Join(", ", descending));
            Console.WriteLine();
            Console.WriteLine("Sorted from smallest to largest:");
            Console.WriteLine(string.Join(", ", ascending));
        }

        private static double ComputeMedian(List<double> sortedNumbersAscending)
        {

            var count = sortedNumbersAscending.Count;
            if (count == 0)
            {
                throw new InvalidOperationException("don't have data number");
            }

            if (count % 2 == 1)
            {
                return sortedNumbersAscending[count / 2];
            }


            var mid1 = sortedNumbersAscending[(count / 2) - 1];
            var mid2 = sortedNumbersAscending[count / 2];
            return (mid1 + mid2) / 2.0;

        }
    }
}


