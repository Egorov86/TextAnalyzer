using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UserContactValidator
{
    public static class ContactValidator
    {
        // Проверка ФИО
        public static bool IsValidFullName(string fullName)
        {
            return !string.IsNullOrWhiteSpace(fullName) && Regex.IsMatch(fullName, @"^[А-Яа-яЁёs]+$");
        }

        // Проверка возраста
        public static bool IsValidAge(string age)
        {
            return int.TryParse(age, out int result) && result >= 0 && result <= 120; // Примерный диапазон возраста
        }

        // Проверка телефона (формат: +7(999)999-99-99)
        public static bool IsValidPhone(string phone)
        {
            return Regex.IsMatch(phone, @"^+7(d{3})d{3}-d{2}-d{2}$");
        }

        // Проверка email
        public static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@s]+@[^@s]+.[^@s]+$");
        }
    }
}
