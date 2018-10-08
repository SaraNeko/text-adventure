using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_adventure
{
    class Player
    {
        string Name { get; }
        List<Item> Inventory;
        public Place Location;

        public Player(string name, Place startingLocation)
        {
            Name = name;
            Inventory = new List<Item>();
            Location = startingLocation;
        }
    }
}
