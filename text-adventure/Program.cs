using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_adventure
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Run();
        }

        public static void SlowPrint(string str, int msDelay = 60)
        {
            foreach (char c in str)
            {
                Console.Write(c);
                if (c == ' ') { continue; }  // Don't wait on spaces - feels unnatural.
                System.Threading.Thread.Sleep(msDelay);
            }
            Console.WriteLine();
        }
    }
}
