using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace QuickTerminalConsole
{
    public class QuickTerminalProgram
    {
        public static event EventHandler<EventArgs> SomethingHappennedEvent;

        static async Task Main(string[] args)
        {
            SomethingHappennedEvent += DummyMethod;

            var exitFlag = false;
            Console.Title = "Quick terminal console";
            var eventInitialized = false;
            var iterationCounter = 0;
            do
            {
                await Task.Delay(millisecondsDelay: 50);
                iterationCounter++;
                if (SomethingHappennedEvent != null)
                    eventInitialized = true;

            } while (!eventInitialized && iterationCounter < 1000);

            if(iterationCounter >= 1000)
                Console.WriteLine("Iteration count exceeded 1000");

            while (!exitFlag)
            {
                Console.WriteLine("write anything except q (which stands for quit)");
                var userInput = Console.ReadLine();
                if (userInput.Equals("q")) exitFlag = true;
                else
                {
                    SomethingHappennedEvent?.Invoke(1, EventArgs.Empty);
                }
            }
        }

        private static void DummyMethod(object sender, EventArgs e)
        {
        }
    }
}
