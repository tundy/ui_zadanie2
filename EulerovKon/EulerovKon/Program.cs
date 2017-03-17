using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace EulerovKon
{
    internal class Program
    {
        private static int _maxSeconds;
        public static int Steps { get; private set; }
        private static bool _work;

        private static readonly Timer Timer = new Timer { AutoReset = false };

        public static bool Start(int x, int y)
        {
            Timer.Interval = 1000 * _maxSeconds;

            _work = true;

            return false;
        }

        static void Main(string[] args)
        {
            _maxSeconds = 15;
            Console.ReadKey(true);
        }
    }
}
