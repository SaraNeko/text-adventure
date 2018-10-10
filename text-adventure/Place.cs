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
        public List<Item> CollectibleItems = new List<Item>();

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
            Program.SlowPrint(Description);
            foreach (Item item in CollectibleItems) { Program.SlowPrint(item.AreaText); }
        }

        public Item GetBlockerItemByName(string name)
        {
            name = name.ToLower();
            foreach (Direction direction in Exits.Values)
            {
                Item blocker = direction.Blocker;
                if (blocker == null) { continue; }

                if (blocker.Name.ToLower() == name) { return blocker; }
            }
            return null;
        }

        public Item GetCollectibleItemByName(string name)
        {
            name = name.ToLower();
            foreach (Item item in CollectibleItems)
            {
                if (item.Name.ToLower() == name) { return item; }
            }
            return null;
        }
    }
}
