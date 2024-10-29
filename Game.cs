using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace game1402_a2_starter
{
    public class GameData
    {
        public string GameName { get; set; } //This is an example of a property; for whatever reason your serializable data objects will need to be written this way
        public string Description { get; set; }
        public List<Room> Rooms { get; set; } //this is only an example. You do not ha

    }

    public class Game(GameData data)
    {
        public GameData _gameData = data;
        public Room currentRoom;

        public List<StaticItems> playerInventory = [];

        public void ProcessString(string enteredString)
        {
            enteredString = enteredString.Trim().ToLower(); //trim any white space from the beginning or end of string and convert to lower case
            string[] commands = enteredString.Split(" "); //split based on spaces. The length of this array will tell you whether you have 1, 2, 3, 4, or more commands
            string response = "Default response";
            switch (commands.Length)
            {
                case 1:
                    OneWordProcess(commands[0]);
                    break;
                case 2:
                    TwoWordProcess(commands);
                    break;
                case 3:
                    ThreeWordProcess(commands);
                    break;
                case 4:
                    break;
            }
        }

        public void OneWordProcess(string enteredString)
        {
            if (enteredString == "investigate")
            {
                currentRoom.ReturnName();
                Console.WriteLine("The items in the room are: ");
                for (int i = 0; i < currentRoom.StaticItems.Count; i++)
                {
                    Console.WriteLine(currentRoom.StaticItems[i].Name);
                }
            }
            else if (enteredString == "sigma")
            {
                Console.WriteLine(_gameData.GameName);
            }
            else if (enteredString == "clear")
            {
                Console.Clear();
            }
            else if (enteredString == "directions")
            {
                PrintAllDirections();
            }
            else if (enteredString == "help")
            {
                Console.WriteLine("One word commands: Investigate, Clear (clears console if you want), Directions (shows all possible direcitons to move)");
                Console.WriteLine("Two word commands: Investigate, Lick, Sniff, Go");
            }
            else
            {
                Console.WriteLine("invalid input, type help for all commands.");
            }
        }

        public void TwoWordProcess(string[] commands)
        {
            // object interaction
            switch (commands[0])
            {
                case "investigate":
                    TwoWordInvestigate(commands);
                    break;
                case "lick":
                    TwoWordLick(commands);
                    break;
                case "sniff":
                    TwoWordSniff(commands);
                    break;
                case "go":
                    TwoWordGo(commands);
                    break;
                default:
                    Console.WriteLine("Command not found. type help to get a list of all commands possible.");
                    break;
            }
            // movement between rooms
        }

        public void ThreeWordProcess(string[] commands)
        {
            switch (commands[0])
            {
                case "pick":
                    if (commands[1] == "up")
                    {
                        ItemPickup(commands);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input");
                    }
                    break;
            }
        }

        private void TwoWordInvestigate(string[] commands)
        {
            if (currentRoom.StaticItems == null || currentRoom?.StaticItems?.Count == 0)
            {
                Console.WriteLine("there is nothing to see here");
                return;
            }

            StaticItems? item = currentRoom?.StaticItems?.Find(i => i.Name.Equals(commands[1], StringComparison.CurrentCultureIgnoreCase));

            if (item != null)
            {
                Console.WriteLine(item.Description);
            }
        }

        private void TwoWordLick(string[] commands)
        {
            if (currentRoom.StaticItems == null || currentRoom?.StaticItems?.Count == 0)
            {
                Console.WriteLine("there is nothing to lick here");
                return;
            }

            StaticItems? item = currentRoom?.StaticItems?.Find(i => i.Name.Equals(commands[1], StringComparison.CurrentCultureIgnoreCase));

            if (item != null)
            {
                Console.WriteLine(item.Taste);
            }
        }

        private void TwoWordSniff(string[] commands)
        {
            if (currentRoom.StaticItems == null || currentRoom?.StaticItems?.Count == 0)
            {
                Console.WriteLine("there is nothing to smell here");
                return;
            }

            StaticItems? item = currentRoom?.StaticItems?.Find(i => i.Name.Equals(commands[1], StringComparison.CurrentCultureIgnoreCase));

            if (item != null)
            {
                Console.WriteLine(item.Smell);
            }
        }

        private void TwoWordGo(string[] commands)
        {
            switch (commands[1])
            {
                case "north":
                    if (currentRoom.North != "0")
                    {
                        currentRoom = _gameData.Rooms.Find(x => x.Name.Equals(currentRoom.North));
                        Console.WriteLine("you are now in the " + currentRoom.Name);
                    }
                    else
                    {
                        Console.WriteLine("theres no where to go in that direction.");
                    }
                    break;
                case "south":
                    if (currentRoom.South != "0")
                    {
                        currentRoom = _gameData.Rooms.Find(x => x.Name.Equals(currentRoom.South));
                        Console.WriteLine("you are now in the " + currentRoom.Name);
                    }
                    else
                    {
                        Console.WriteLine("theres no where to go in that direction.");
                    }
                    break;
                case "west":
                    if (currentRoom.West != "0")
                    {
                        currentRoom = _gameData.Rooms.Find(x => x.Name.Equals(currentRoom.West));
                        Console.WriteLine("you are now in the " + currentRoom.Name);
                    }
                    else
                    {
                        Console.WriteLine("theres no where to go in that direction.");
                    }
                    break;
                case "east":
                    if (currentRoom.East != "0")
                    {
                        currentRoom = _gameData.Rooms.Find(x => x.Name.Equals(currentRoom.East));
                        Console.WriteLine("you are now in the " + currentRoom.Name);
                    }
                    else
                    {
                        Console.WriteLine("theres no where to go in that direction.");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid direction, only north south west and east.");
                    break;
            }
        }

        private void PrintAllDirections()
        {
            if (currentRoom.North != "0")
            {
                Console.WriteLine(currentRoom.North + " is north.");
            }
            if (currentRoom.South != "0")
            {
                Console.WriteLine(currentRoom.South + " is south.");
            }
            if (currentRoom.West != "0")
            {
                Console.WriteLine(currentRoom.West + " is west.");
            }
            if (currentRoom.East != "0")
            {
                Console.WriteLine(currentRoom.East + " is east.");
            }

        }

        private void ItemPickup(string[] commands)
        {

            StaticItems? item = currentRoom.StaticItems.Find(i => i.Name.Equals(commands[2], StringComparison.CurrentCultureIgnoreCase));


            if (item != null && item.objectType == StaticItems.Types.Pickup)
            {
                playerInventory.Add(item);
                currentRoom.StaticItems.Remove(item);
                Console.WriteLine("Picked up " + item.Name);
            }
            else
            {
                Console.WriteLine("item cannot be picked up.");
            }
        }

    }
}
