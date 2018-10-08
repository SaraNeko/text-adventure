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

        Dictionary<string, Place> Places = new Dictionary<string, Place>();
        Player Player;

        bool IsRunning;     // Shows whether or not the game is still running.

        public Game()
        {
            IsRunning = true;

            SetupMap();
        }

        private void SetupMap()
        {
            Item StoneWall = new Item("A large stone wall stands in your way.", "", isBlocking: true);

            Place Room1 = new Place("Room 1", "Looks very oney.");
            Place Room2 = new Place("Room 2", "Looks very twoey.");

            Room1.Exits[Direction.NORTH] = new Direction(Room2);
            Room1.Exits[Direction.EAST] = new Direction(StoneWall);
            Room1.Exits[Direction.SOUTH] = new Direction(StoneWall);
            Room1.Exits[Direction.WEST] = new Direction(StoneWall);

            Room2.Exits[Direction.NORTH] = new Direction(StoneWall);
            Room2.Exits[Direction.EAST] = new Direction(StoneWall);
            Room2.Exits[Direction.SOUTH] = new Direction(Room1);
            Room2.Exits[Direction.WEST] = new Direction(StoneWall);

            Places["Room1"] = Room1;
            Places["Room2"] = Room2;
        }
        // this is the game class :3 you're a crute. ♥

        public void Run()
        {
            Console.WriteLine("Welcome to Kittu Land >;3\n");

            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine();

            Player = new Player(name, Places["Room1"]);

            DescribeLocation();

            while (IsRunning)
            {
                ProcessCommand();
            }

            Console.WriteLine("Thanks for coming to Kittu Land >:3\n You'll be back soon! (Press Enter to exit.)");
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
                case "q":
                    Quit();
                    break;

                case "look":
                    DescribeLocation();
                    break;

                case "go":
                case "move":
                case "run":
                    Move(commandWords[1]);
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
                return;
            }

            Player.Location = exit.Destination;
            DescribeLocation();
        }
    }
}
