using ConsoleCalc.Input;
using ConsoleCalc.Core;

namespace ConsoleCalc
{
    internal class Program
    {
        protected static States currentState;
        protected static States lastState = States.Start;
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

        public static States LastState
        {
            get { return lastState; }
            set { lastState = value; }
        }


        static void Main(string[] args)
        {
            ConsoleInputs consoleInputs = new ConsoleInputs(new Calculator());
            CurrentState = States.Start;
        }
    }
}
