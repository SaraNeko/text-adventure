using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_adventure
{
    class Item
    {
        public string Name;
        public string Description;
        public string ExtendedDescription;
        public string AreaText;
        public bool CanBePickedUp { get; }
        public bool IsBlocking;

        public Item(string name, string description, string extendedDescription, string areaText = null, bool canBePickedUp = false, bool isBlocking = false)
        {
            Name = name;
            Description = description;
            ExtendedDescription = extendedDescription;
            AreaText = areaText;
            CanBePickedUp = canBePickedUp;
            IsBlocking = isBlocking;
        }

        public void Describe()
        {
            Program.SlowPrint(Description);
        }

        public void Examine()
        {
            Program.SlowPrint(ExtendedDescription);
        }
    }
}
