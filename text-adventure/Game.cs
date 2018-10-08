using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_adventure
{
    class Game
    {
        static string PROMPT = "\n> ";

        Player Player;
        Place Room1;
        Item Room1DoorKey;
        Place Room2;
        Place Room3;

        bool IsRunning;     // Shows whether or not the game is still running.

        public Game()
        {
            IsRunning = true;

            SetupMap();
        }

        private void SetupMap()
        {
            Item StoneWall = new Item("Stone Wall", "A large stone wall stands in your way.", "The wall won't budge.", isBlocking: true);

            Room1 = new Place("Room 1", "Looks very oney. There is an arch to the north leading to another room, and a small wooden door to the west.");
            Room2 = new Place("Room 2", "Looks very twoey. There is an arch to the south leading to another room.");
            Room3 = new Place("Room 3", "Looks very threey. There is a small wooden door to the east.");

            Room1.Exits[Direction.NORTH] = new Direction(Room2);
            Room1.Exits[Direction.EAST] = new Direction(StoneWall);
            Room1.Exits[Direction.SOUTH] = new Direction(StoneWall);
            Item Room1LockedDoor = new Item("Door", "There is a small wooden door in the stone wall.",
                                            "The door is locked.", isBlocking: true);
            Room1DoorKey = new Item("Key", "A Key.", "Upon further inspection, you notice a faded '3' symbol on the key.",
                                    areaText: "There is a key on the ground.", canBePickedUp: true);
            Room1.Exits[Direction.WEST] = new Direction(Room3, Room1LockedDoor);

            Room2.Exits[Direction.NORTH] = new Direction(StoneWall);
            Room2.Exits[Direction.EAST] = new Direction(StoneWall);
            Room2.Exits[Direction.SOUTH] = new Direction(Room1);
            Room2.Exits[Direction.WEST] = new Direction(StoneWall);
            Room2.CollectibleItems.Add(Room1DoorKey);

            Room3.Exits[Direction.NORTH] = new Direction(StoneWall);
            Room3.Exits[Direction.EAST] = new Direction(Room1);
            Room3.Exits[Direction.SOUTH] = new Direction(StoneWall);
            Room3.Exits[Direction.WEST] = new Direction(StoneWall);
        }
        // this is the game class :3 you're a crute. ♥

        public void Run()
        {
            Console.WriteLine("Welcome to Kittu Land >;3\nYou find yourself in a strange room...\n");

            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine();

            Player = new Player(name, Room1);

            DescribeLocation();

            while (IsRunning)
            {
                ProcessCommand();
            }

            Console.WriteLine("Thanks for coming to Kittu Land >:3\nYou'll be back soon! (Press Enter to exit.)");
            Console.ReadLine();
        }

        private void ProcessCommand()
        {
            Console.Write(PROMPT);
            string command = Console.ReadLine().ToLower();

            string[] commandWords = command.Split(' ');
            string action = commandWords[0];

            switch (action)
            {
                case "quit":
                case "exit":
                case "bye":
                    Quit();
                    break;

                case "look":
                    DescribeLocation();
                    break;

                case "go":
                case "move":
                case "walk":
                case "run":
                    if (commandWords.Length == 1)
                    {
                        Console.WriteLine($"{action} where?");
                        break;
                    }
                    Move(commandWords[1]);
                    break;

                case "inventory":
                case "items":
                    ShowInventory();
                    break;

                case "take":
                case "get":
                case "grab":
                    if (commandWords.Length == 1)
                    {
                        Console.WriteLine($"{action} what?");
                        break;
                    }
                    Take(commandWords[1]);
                    break;

                case "drop":
                case "throw":
                    if (commandWords.Length == 1)
                    {
                        Console.WriteLine($"{action} what?");
                        break;
                    }
                    Drop(commandWords[1]);
                    break;

                case "examine":
                case "inspect":
                    if (commandWords.Length == 1)
                    {
                        Console.WriteLine($"{action} what?");
                        break;
                    }
                    Examine(commandWords[1]);
                    break;

                case "use":
                    if (commandWords.Length == 1)
                    {
                        Console.WriteLine("Use what on what?");
                        break;
                    }
                    else if (new int[] { 2, 3 }.Contains(commandWords.Length))
                    {
                        Console.WriteLine($"Use {commandWords[1]} on what?");
                    }
                    if (!(commandWords[2] == "on")) { goto default; }
                    Use(commandWords[1], commandWords[3]);
                    break;

                case "hello":
                case "hi":
                case "hey":
                case "hej":
                    Console.WriteLine("You hear the echo of your own greeting."); // do diff answers.
                    break;

                case "thanks":
                case "thank":
                    Console.WriteLine("Somehow you don't feel welcome.");
                    break;

                case "ok":
                case "okay":
                case "okey":
                case "okej":
                    Console.WriteLine("Ok.");
                    break;

                default:
                    Console.WriteLine("ALET. I DO NOT UNDERSTEND THIS COMMAND.");
                    break;
            }
        }

        private void Quit()
        {
            IsRunning = false;
        }

        private void DescribeLocation()
        {
            Player.Location.Describe();
        }

        private void Move(string direction)
        {
            if (!(new string[] { Direction.NORTH, Direction.EAST, Direction.SOUTH, Direction.WEST }.Contains(direction)))
            {
                Console.WriteLine($"I don't know the direction '{direction}'.");
                return;
            }

            Direction exit = Player.Location.Exits[direction];

            Item blocker = exit.Blocker;
            if (blocker != null && blocker.IsBlocking)
            {
                blocker.Describe();
                blocker.Examine();
                return;
            }

            Player.Location = exit.Destination;
            DescribeLocation();
        }

        private void ShowInventory()
        {
            if (Player.Inventory.Count == 0)
            {
                Console.WriteLine("You have no items.");
                return;
            }

            Console.WriteLine("Inventory:");
            foreach (Item item in Player.Inventory)
            {
                Console.WriteLine($"- {item.Name}");
            }
        }

        private void Take(string itemName)
        {
            Item item = Player.Location.GetCollectibleItemByName(itemName);
            if (item == null)
            {
                Console.WriteLine($"There is no '{itemName}'.");
                return;
            }

            if (!item.CanBePickedUp)
            {
                Console.WriteLine("You can't pick that up.");
                return;
            }

            Player.Location.CollectibleItems.Remove(item);
            Player.Inventory.Add(item);
            Console.WriteLine($"Took {item.Name}.");
        }

        private void Drop(string itemName)
        {
            Item item = Player.GetInventoryItemByName(itemName);
            if (item == null)
            {
                Console.WriteLine($"You don't have a '{itemName}'.");
                return;
            }

            Player.Inventory.Remove(item);
            Player.Location.CollectibleItems.Add(item);
            Console.WriteLine($"Dropped {item.Name}.");
        }

        private void Examine(string itemName)
        {
            Item item = Player.GetInventoryItemByName(itemName)
                ?? Player.Location.GetCollectibleItemByName(itemName)
                ?? Player.Location.GetBlockerItemByName(itemName);
            if (item == null)
            {
                Console.WriteLine($"There is no '{itemName}'.");
                return;
            }

            item.Examine();
        }

        /* Use the item with the first name on the item with the second name. */
        private void Use(string item1Name, string item2Name)
        {
            Item item1 = Player.GetInventoryItemByName(item1Name);
            if (item1 == null)
            {
                Console.WriteLine($"You don't have a '{item1Name}'.");
                return;
            }

            Item item2 = Player.Location.GetBlockerItemByName(item2Name)
                         ?? Player.Location.GetCollectibleItemByName(item2Name);
            if (item2 == null)
            {
                Console.WriteLine($"There is no '{item2Name}'.");
                return;
            }

            if (Player.Location == Room1)
            {
                Item Door = Room1.Exits[Direction.WEST].Blocker;
                if (item1 == Room1DoorKey && item2 == Door)
                {
                    if (!Door.IsBlocking)
                    {
                        Console.WriteLine("The door is already unlocked.");
                        return;
                    }

                    Door.IsBlocking = false;
                    Door.ExtendedDescription = "The door is unlocked.";
                    Console.WriteLine("You unlock the door.");
                    return;
                }
            }

            Console.WriteLine("Nothing interesting happens.");
        }
    }
}
