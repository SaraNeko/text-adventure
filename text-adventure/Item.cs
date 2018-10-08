using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_adventure
{
    class Item
    {
        string Description;
        string ExtendedDescription;
        public bool CanBePickedUp { get; }
        public bool IsBlocking { get; }

        public Item(string description, string extendedDescription, bool canBePickedUp = false, bool isBlocking = false)
        {
            Description = description;
            ExtendedDescription = extendedDescription;
            CanBePickedUp = canBePickedUp;
            IsBlocking = isBlocking;
        }

        public void Describe()
        {
            Console.WriteLine(Description);
        }
    }
}
