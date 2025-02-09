using System;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        int number = 8;
        long result = 1;
        int digitCount = 0;
        int digitSum = 0;
        long[] particalFactorials = new long[number];

        for (int i = 0; i < particalFactorials.Length; i++)
        {
            particalFactorials[i] = 1;
        }

        Parallel.For(1, number + 1, i =>
        {
            long factorial = 1;
            for (int j = 1; j <= i; j++)
            {
                factorial += j;
            }

            particalFactorials[i - 1] = factorial;
        });

        for (int i = 0; i < particalFactorials.Length; i++)
        {
            result += particalFactorials[i];
        }
         // параллельные операции
         // чере инвок паралелю операции
        Parallel.Invoke(
            () => {
                digitCount = CountDigits(result);
            },
            () => {
                digitSum = SumDigits(result);
            }
        );

        Console.WriteLine($"факториал числа {number} равен {result}");
        Console.WriteLine($"Количество цифр в результате: {digitCount}");
        Console.WriteLine($"Сумма цифр в результате: {digitSum}");
    }

    static int CountDigits(long number)
    {
        return number.ToString().Length;
    }

    static int SumDigits(long number)
    {
        int sum = 0;
        while (number > 0)
        {
            sum += (int)(number % 10);
            number /= 10;
        }
        return sum;
    }
}
