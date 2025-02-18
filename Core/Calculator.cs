using ConsoleCalc.Setup;

namespace ConsoleCalc.Core
{
    delegate double Operation(double x, double y);
    public class Calculator
    {
        private double _operator1;
        private double _operator2;
        private string? _operationSign;
        private double _result;

        private Operation? operation;

        public double Operator1
        {
            get { return _operator1; }
            private set { _operator1 = value; }
        }
        public double Operator2
        {
            get { return _operator2; }
            private set { _operator2 = value; }
        }
        public string? OperationSign
        {
            get { return _operationSign; }
            private set { _operationSign = value; }
        }
        public double Result
        {
            get { return _result; }
            private set { _result = value; }
        }

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
