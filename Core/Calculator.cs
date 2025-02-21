using ConsoleCalc.Setup;

namespace ConsoleCalc.Core
{
    delegate double Operation(double x, double y);
    internal class Calculator
    {
        private Operation? operation;

        public double Operator1 { get; private set; }
        public double Operator2 { get; private set; }
        public string? OperationSign { get; private set; }

        public double Result { get; private set; }

        public void InputOperator(string str)
        {
            double _operator;
            bool _isParsed;

            _isParsed = double.TryParse(str, out _operator);

            if (!_isParsed)
            {
                CommonText.ShowErrorMessage("Это не число, введите ещё раз!");
                Program.CurrentState = Program.CurrentState;
                return;
            }
            else
            {
                if (Program.CurrentState == States.WaitForFisrtOperator)
                {
                    Operator1 = _operator;

                    Program.CurrentState = States.WaitForOperationSign;
                }
                if (Program.CurrentState == States.WaitForSecondOperator)
                {
                    Operator2 = _operator;

                    if (operation == Operations.Divide)
                    {
                        if (Operator2 == 0)
                        {
                            CommonText.ShowErrorMessage("На ноль делить нельзя!");
                            Program.CurrentState = Program.CurrentState;
                            return;
                        }
                    }

                    Program.CurrentState = States.ShowingResult;
                }
            }
        }

        public void InputOperation(string str)
        {
            bool _isOperation = Operations.TryParseOperation(str, out operation);

            if (_isOperation)
            {
                OperationSign = str;
                Program.CurrentState = States.WaitForSecondOperator;
            }
            else
            {
                Program.CurrentState = Program.CurrentState;
            }
        }

        public void ProceedOperation()
        {
            if (operation != null)
                Result = operation(Operator1, Operator2);

            ShowResultText();

            Program.CurrentState = States.WaitForFisrtOperator;
        }

        private void ShowResultText()
        {
            //Console.Clear(); __________________________________
            Console.WriteLine("----------------------------------\nX = {0}\nY = {1}\n----------------------------------", Operator1, Operator2);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Результат: {0} {1} {2} = {3}", CommonText.GetFormatedOperator(Operator1), OperationSign, CommonText.GetFormatedOperator(Operator2), Result);
            Console.ForegroundColor = Settings.DefaultColor;
            Console.WriteLine("==================================\n");
        }
    }
}
