using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_adventure
{
    class Player
    {
        public string Name { get; }
        public List<Item> Inventory;
        public Place Location;

        public Player(string name, Place startingLocation)
        {
            Name = name;
            Inventory = new List<Item>();
            Location = startingLocation;
        }

        public Item GetInventoryItemByName(string name)
        {
            name = name.ToLower();
            foreach (Item item in Inventory)
            {
                if (item.Name.ToLower() == name) { return item; }
            }
            return null;
        }
    }
}
