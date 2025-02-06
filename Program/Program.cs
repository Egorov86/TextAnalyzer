using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // надо указать путь к файлу
        string filePath = "numbers.txt";

        // Считываю числа из файла
        List<int> numbers = ReadNumbersFromFile(filePath);

        // Находим макс. длину возрастающей последовательности
        int maxLength = FindLongestIncreasingSubsequenceLength(numbers);

        Console.WriteLine($"Длина самой большой возрастающей последовательности: {maxLength}");
    }

    static List<int> ReadNumbersFromFile(string filePath)
    {
        var numbers = new List<int>();

        try
        {
            // Чтение всех строк из файла и преобразование их в список целых чисел через foreach
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var nums = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                               .Select(int.Parse);
                numbers.AddRange(nums);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
        }

        return numbers;
    }

    static int FindLongestIncreasingSubsequenceLength(List<int> numbers)
    {
        if (numbers == null || numbers.Count == 0)
            return 0;

        // Массив для хранения длин возрастающих последовательностей
        int[] lengths = new int[numbers.Count];
        for (int i = 0; i < lengths.Length; i++)
            lengths[i] = 1; // Каждое число по умолчанию считается длиной 1

        // Использую PLINQ для параллельной обработки, возвращаю макс число.
        var maxLength = numbers.AsParallel()
            .Select((num, index) => new { num, index })
            .Aggregate(1, (currentMax, item) =>
            {
                for (int j = 0; j < item.index; j++)
                {
                    if (numbers[j] < item.num)
                    {
                        lengths[item.index] = Math.Max(lengths[item.index], lengths[j] + 1);
                    }
                }
                return Math.Max(currentMax, lengths[item.index]);
            });

        return maxLength;
    }
}
