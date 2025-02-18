using ConsoleCalc.Setup;
using ConsoleCalc.Core;

namespace ConsoleCalc.Input
{
    internal class ConsoleInputs
    {
        public Calculator calc;
        public ConsoleInputs(Calculator calc)
        {
            this.calc = calc;
            Program.stateUpdate = Update;
        }

        private void Update()
        {
            //Console.WriteLine("State has updated to {0}...", Program.CurrentState);

            if (Program.CurrentState == States.Start)
            {
                Console.Clear();
                CommonText.ShowInitText();
            }

            if (Program.CurrentState == States.Settings)
                Console.WriteLine("\n--------- НАСТРОЙКИ ---------\n");

            Input();
        }

        public void Input()
        {
            string? str;

            switch (Program.CurrentState)
            {
                case States.Start:
                    Console.Write("Введите команду /start, чтобы начать: ");
                    str = Console.ReadLine();
                    if (!CheckCommands(str))
                        Input();
                    break;
                case States.WaitForFisrtOperator:
                    Console.Write("Введите X: ");
                    str = Console.ReadLine();
                    if (!CheckCommands(str))
                        calc.InputOperator(str);
                    break;
                case States.WaitForOperationSign:
                    Console.Write("Введите символ операции: ");
                    str = Console.ReadLine();
                    if (!CheckCommands(str))
                        calc.InputOperation(str);
                    break;
                case States.WaitForSecondOperator:
                    Console.Write("Введите Y: ");
                    str = Console.ReadLine();
                    if (!CheckCommands(str))
                        calc.InputOperator(str);
                    break;
                case States.ShowingResult:
                    calc.ProceedOperation();
                    break;
                case States.Settings:
                    Console.Write("Введите цвет: ");
                    str = Console.ReadLine();
                    if (!CheckCommands(str))
                        Input();
                    break;
            }
        }

        public bool CheckCommands(string str)
        {
            if (Program.CurrentState == States.Settings)
            {
                switch (str)
                {
                    case "/back":
                        Program.CurrentState = Program.LastState;
                        return true;
                    case "white":
                        Settings.SetNewColor(str);
                        Update();
                        return true;
                    case "yellow":
                        Settings.SetNewColor(str);
                        Update();
                        return true;
                    case "cyan":
                        Settings.SetNewColor(str);
                        Update();
                        return true;
                    case "blue":
                        Settings.SetNewColor(str);
                        Update();
                        return true;
                    case "magenta":
                        Settings.SetNewColor(str);
                        Update();
                        return true;
                }
            }

            switch (str)
            {
                case "/start":
                    Console.WriteLine();
                    Program.CurrentState = States.WaitForFisrtOperator;
                    return true;
                case "/stop":
                    Program.CurrentState = States.Start;
                    return true;
                case "/cancel":
                    Console.WriteLine();
                    if (Program.CurrentState == States.Start) return false;
                    if (Program.CurrentState == States.Settings) Program.CurrentState = Program.LastState;
                    Program.CurrentState = States.WaitForFisrtOperator;
                    return true;
                case "/clear":
                    Console.Clear();
                    Update();
                    return true;
                case "/settings":
                    if (Program.CurrentState != States.Settings) Program.LastState = Program.CurrentState;
                    Program.CurrentState = States.Settings;
                    return true;
                case "/help":
                    CommonText.ShowInitText();
                    Update();
                    return true;
                case "/exit":
                    Environment.Exit(0);
                    return true;
                default: return false;
            }
        }

        public void InputOperator(string str)
        {
            double _operator;
            bool _isParsed;

            _isParsed = double.TryParse(str, out _operator);

            if (!_isParsed)
            {
                CheckCommands(str);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Это не число, введите ещё раз!");
                Console.ForegroundColor = Settings.DefaultColor;
                Input();
                return;
            }
            else
            {
                Program.CurrentState = States.WaitForOperationSign;
            }
        }
    }
}
