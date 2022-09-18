using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_12.Models.Client
{
    public class Passport
    {
        public const int MinSeriesValue = 10;
        public const int MaxSeriesValue = 9999;

        public const int MinNumberValue = 100;
        public const int MaxNumberValue = 999999;

        public int Serie { get; set; }

        public int Number { get; set; }

        /// <summary>
        /// Проверка, являются ли вводимые данные серией паспорта
        /// </summary>
        /// <param name="series">Серия</param>
        /// <returns></returns>
        public static bool IsSeries(string value)
        {
            int serie;

            if (!int.TryParse(value, out serie))
                return false;

            if (serie < MinSeriesValue || serie > MaxSeriesValue)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Проверка, являются ли вводимые данные номером паспорта
        /// </summary>
        /// <param name="number">Номер</param>
        /// <returns></returns>
        public static bool IsNumber(string value)
        {
            int number;

            if (!int.TryParse(value, out number))
                return false;

            if (number < MinNumberValue || number > MaxNumberValue)
            {
                return false;
            }
            return true;
        }

        public Passport() { }

        /// <summary>
        /// Создаем пасспорт с серией и номером
        /// </summary>
        /// <param name="series">Серия</param>
        /// <param name="number">Номер</param>
        public Passport(int serie, int number)
        {
            Serie = serie;
            Number = number;
        }

        public override string ToString()
        {
            return $"{Serie}-{Number}";
        }
    }
}
