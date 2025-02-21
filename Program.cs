using ConsoleCalc.Input;
using ConsoleCalc.Core;

namespace ConsoleCalc
{
    internal class Program
    {
        static States currentState;
        public static Action? stateUpdate;
        public static States CurrentState
        {
            get { return currentState; }
            set
            {
                currentState = value;
                stateUpdate?.Invoke();
            }
        }

        public static States LastState {get; set;} = States.Start;


        static void Main(string[] args)
        {
            ConsoleInputs consoleInputs = new ConsoleInputs(new Calculator());
            CurrentState = States.Start;
        }
    }
}
