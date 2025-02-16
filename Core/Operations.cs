using ConsoleCalc.Setup; //For settings and Text

namespace ConsoleCalc.Core
{
    static class Operations
    {
        public static bool TryParseOperation(string str, out Operation? operation)
        {
            switch (str)
            {
                case "+":
                    operation = Add;
                    break;
                case "-":
                    operation = Substract;
                    break;
                case "*":
                    operation = Multiply;
                    break;
                case "/":
                    operation = Divide;
                    break;
                case "^":
                    operation = Pow;
                    break;
                default:
                    CommonText.ShowErrorMessage("Неопознанный символ!");
                    operation = null;
                    return false;
            }

            return true;
        }

        public static double Add(double x, double y) => x + y;
        public static double Substract(double x, double y) => x - y;
        public static double Multiply(double x, double y) => x * y;
        public static double Divide(double x, double y) => x / y;
        public static double Pow(double x, double y) => Math.Pow(x, y);
    }
}

