using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserContactValidator;

namespace ContactValidationApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите ФИО:");
            string fullName = Console.ReadLine();
            Console.WriteLine($"ФИО корректно: {ContactValidator.IsValidFullName(fullName)}");

            Console.WriteLine("Введите возраст:");
            string age = Console.ReadLine();
            Console.WriteLine($"Возраст корректен: {ContactValidator.IsValidAge(age)}");

            Console.WriteLine("Введите телефон в формате +7(999)999-99-99:");
            string phone = Console.ReadLine();
            Console.WriteLine($"Телефон корректен: {ContactValidator.IsValidPhone(phone)}");

            Console.WriteLine("Введите email:");
            string email = Console.ReadLine();
            Console.WriteLine($"Email корректен: {ContactValidator.IsValidEmail(email)}");
        }
    }
}