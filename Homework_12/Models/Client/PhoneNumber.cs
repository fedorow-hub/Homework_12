using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Homework_12.Models.Client
{
    public class PhoneNumber
    {
        private string _number;
        /// <summary>
        /// Текстовое представление телефонного номера
        /// </summary>
        public string Number
        {
            get { return _number; }
            set { _number = value; }
        }

        public PhoneNumber() { }

        /// <summary>
        /// Создаем номер телефона из текстовой строки
        /// </summary>
        /// <param name="number"></param>
        public PhoneNumber(string number)
        {
            SetNumber(number);
        }

        /// <summary>
        /// Проверяем, является ли вводимая строка номером телефона
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsPhoneNumber(string number)
        {
            if (Regex.Match(number, @"^(\+[0-9]{11})$").Success || Regex.Match(number, @"^([0-9]{11})$").Success)
                return true;
            return false;
        }

        /// <summary>
        /// Установка номера телефона
        /// </summary>
        /// <param name="number"></param>
        private void SetNumber(string number)
        {
            CheckNumber(number);
            _number = number;
        }

        /// <summary>
        /// Проверка валидности входных данных
        /// </summary>
        /// <param name="number"></param>
        private void CheckNumber(string number)
        {
            if (string.IsNullOrEmpty(number) || string.IsNullOrWhiteSpace(number))
            {
                throw new ArgumentException($"Номер \"{nameof(number)}\" не может быть пустым или пробелом");
            }

            if (!IsPhoneNumber(number))
            {
                throw new ArgumentException($"Строка \"{nameof(number)}\" не является номером телефона");
            }
        }

        public override string ToString()
        {
            return $"{Number}";
        }
    }
}
