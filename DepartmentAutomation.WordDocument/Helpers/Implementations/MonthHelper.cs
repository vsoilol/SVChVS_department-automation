using System;
using DepartmentAutomation.WordDocument.Helpers.Interfaces;

namespace DepartmentAutomation.WordDocument.Helpers.Implementations
{
    internal class MonthHelper : IMonthHelper
    {
        public string GetMonthInRussian(int monthNumber)
        {
            return monthNumber switch
            {
                1 => "января",
                2 => "февраля",
                3 => "марта",
                4 => "апреля",
                5 => "мая",
                6 => "июня",
                7 => "июля",
                8 => "августа",
                9 => "сентября",
                10 => "октября",
                11 => "ноября",
                12 => "декабря",
                _ => throw new ArgumentOutOfRangeException("Month number is not valid")
            };
        }
    }
}