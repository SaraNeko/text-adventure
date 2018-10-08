using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_adventure
{
    class Place
    {
        string Name;
        string Description;

        public Dictionary<string, Direction> Exits = new Dictionary<string, Direction>();

        public Place(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public void Describe()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(Name);
            Console.ResetColor();
            Console.WriteLine(Description);
        }
    }
}
