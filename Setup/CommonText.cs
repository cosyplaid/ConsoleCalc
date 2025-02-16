using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalc.Setup
{
    static class CommonText
    {
        public static void ShowErrorMessage(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ForegroundColor = Settings.DefaultColor;
        }

        public static string GetFormatedOperator(double value)
        {
            return value < 0 ? $"({value})" : value.ToString();
        }
    }
}