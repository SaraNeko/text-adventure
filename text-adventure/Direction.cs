using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_adventure
{
    class Direction
    {
        public Place Destination { get; }
        public Item Blocker { get; }

        public static string NORTH = "north";
        public static string EAST = "east";
        public static string SOUTH = "south";
        public static string WEST = "west";

        public Direction(Place destination, Item blocker)
        {
            Destination = destination;
            Blocker = blocker;
        }

        public Direction(Place destination)
        {
            Destination = destination;
            Blocker = null;
        }

        public Direction(Item blocker)
        {
            Destination = null;
            Blocker = blocker;
        }
    }
}
