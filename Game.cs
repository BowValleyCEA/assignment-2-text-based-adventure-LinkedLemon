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
                    FourWordProcess(commands);
                    break;
                default:
                    Console.WriteLine("Invalid input, type help for all commands");
                    break;
            }
        }

        public void OneWordProcess(string enteredString)
        {
            if (enteredString == "inspect")
            {
                currentRoom.ReturnName();
                Console.WriteLine(currentRoom.Description);
                Console.WriteLine();
                Console.WriteLine("Inside this room is: ");
                for (int i = 0; i < currentRoom.StaticItems.Count; i++)
                {
                    if (currentRoom.StaticItems[i].isAvailable)
                    {
                        Console.WriteLine(currentRoom.StaticItems[i].Reference);
                    }
                }
                return;
            }
            if (enteredString == "sigma")
            {
                Console.WriteLine(_gameData.GameName);
                return;
            }
            if (enteredString == "clear")
            {
                Console.Clear();
                return;
            }
            if (enteredString == "directions")
            {
                PrintAllDirections();
                return;
            }
            if (enteredString == "help")
            {
                Console.WriteLine("One word commands: Inspect, Clear (clears console if you want),");
                Console.WriteLine("Directions (shows all possible direcitons to move), Inventory (shows all items you're carrying)");
                Console.WriteLine("Two word commands: Touch X, Lick X, Sniff X, Go X, Use X");
                Console.WriteLine("Three word commands: Pick up X");
                Console.WriteLine("Four word commands: Use X on Y");
                Console.WriteLine();
                Console.WriteLine("Extra tip: the action use is for objects containing another object");
                Console.WriteLine("Such as a chest holding a item, type use chest to get that item");
                return;
            }
            if (enteredString == "inventory")
            {
                if (playerInventory.Count != 0)
                {
                    for (int i = 0; i < playerInventory.Count; i++)
                    {
                        Console.WriteLine($"{playerInventory[i].Name}");
                    }
                }
                else
                {
                    Console.WriteLine("Nothing is in my inventory.");
                }
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
                case "touch":
                    TwoWordTouch(commands);
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
                case "use":
                    TwoWordUse(commands);
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
                        ThreeItemPickup(commands);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input, type help for all commands");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid input, type help for all commands");
                    break;
            }
        }

        private void FourWordProcess(string[] commands)
        {
            if (commands[0] == "use" && commands[2] == "on")
            {
                FourUseOn(commands);
            }
            else
            {
                Console.WriteLine("Invalid input, type help for all commands");
            }
        }

        private void TwoWordTouch(string[] commands)
        {
            if (currentRoom.StaticItems == null || currentRoom?.StaticItems?.Count == 0)
            {
                Console.WriteLine("There is nothing in here.");
                return;
            }

            StaticItems? item = currentRoom?.StaticItems?.Find(i => i.Name.Equals(commands[1], StringComparison.CurrentCultureIgnoreCase));
            StaticItems? inventroyItem = playerInventory?.Find(i => i.Name.Equals(commands[1], StringComparison.CurrentCultureIgnoreCase));

            if (item != null && item.isAvailable != false)
            {
                Console.WriteLine(item.Description[item.state]);
            }
            else if (inventroyItem != null && inventroyItem.isAvailable != false)
            {
                Console.WriteLine(inventroyItem.Description[inventroyItem.state]);
            }
            else
            {
                Console.WriteLine("I cant touch that.");
            }
        }

        private void TwoWordLick(string[] commands)
        {
            if (currentRoom.StaticItems == null || currentRoom?.StaticItems?.Count == 0)
            {
                Console.WriteLine("There's nothing to lick here");
                return;
            }

            StaticItems? item = currentRoom?.StaticItems?.Find(i => i.Name.Equals(commands[1], StringComparison.CurrentCultureIgnoreCase));
            StaticItems? inventroyItem = playerInventory?.Find(i => i.Name.Equals(commands[1], StringComparison.CurrentCultureIgnoreCase));

            if (item != null && item.isAvailable != false)
            {
                Console.WriteLine(item.Taste);
            }
            else if (inventroyItem != null && inventroyItem.isAvailable != false)
            {
                Console.WriteLine(inventroyItem.Taste);
            }
            else
            {
                Console.WriteLine("I cant lick that.");
            }
        }

        private void TwoWordSniff(string[] commands)
        {
            if (currentRoom.StaticItems == null || currentRoom?.StaticItems?.Count == 0)
            {
                Console.WriteLine("There's nothing to smell here");
                return;
            }

            StaticItems? item = currentRoom?.StaticItems?.Find(i => i.Name.Equals(commands[1], StringComparison.CurrentCultureIgnoreCase));
            StaticItems? inventroyItem = playerInventory?.Find(i => i.Name.Equals(commands[1], StringComparison.CurrentCultureIgnoreCase));

            if (item != null && item.isAvailable != false)
            {
                Console.WriteLine(item.Smell);
            }
            else if (inventroyItem != null && inventroyItem.isAvailable != false)
            {
                Console.WriteLine(inventroyItem.Smell);
            }
            else
            {
                Console.WriteLine("I cant sniff that.");
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
                        Console.WriteLine("You are now in the " + currentRoom.Name);
                    }
                    else
                    {
                        Console.WriteLine("There's no where to go in that direction.");
                    }
                    break;
                case "south":
                    if (currentRoom.South != "0")
                    {
                        currentRoom = _gameData.Rooms.Find(x => x.Name.Equals(currentRoom.South));
                        Console.WriteLine("You are now in the " + currentRoom.Name);
                    }
                    else
                    {
                        Console.WriteLine("There's no where to go in that direction.");
                    }
                    break;
                case "west":
                    if (currentRoom.West != "0")
                    {
                        currentRoom = _gameData.Rooms.Find(x => x.Name.Equals(currentRoom.West));
                        Console.WriteLine("You are now in the " + currentRoom.Name);
                    }
                    else
                    {
                        Console.WriteLine("There's no where to go in that direction.");
                    }
                    break;
                case "east":
                    if (currentRoom.East != "0")
                    {
                        currentRoom = _gameData.Rooms.Find(x => x.Name.Equals(currentRoom.East));
                        Console.WriteLine("You are now in the " + currentRoom.Name);
                    }
                    else
                    {
                        Console.WriteLine("There's no where to go in that direction.");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid direction, only north south west and east.");
                    break;
            }

            if (currentRoom.Name == "Exit room")
            {
                Console.WriteLine("You have left the cabin and as such won! you may continue exploring the cabin if you wish.");
            }
        }

        private void TwoWordUse(string[] commands)
        {
            if (currentRoom.StaticItems == null || currentRoom?.StaticItems?.Count == 0)
            {
                Console.WriteLine("There's nothing to use here");
                return;
            }

            StaticItems? item = currentRoom?.StaticItems?.Find(i => i.Name.Equals(commands[1], StringComparison.CurrentCultureIgnoreCase));

            if (item != null && item.state == 1)
            {
                if (item.objectType == StaticItems.Types.Interaction)
                {
                    Console.WriteLine(item.Description[2]);

                    if (item.Name.ToLower() == "exit")
                    {
                        currentRoom.South = "Exit room";
                        return;
                    }

                    StaticItems internalItem = currentRoom.StaticItems.Find(i => i.Name.Equals(item.HoldingItem, StringComparison.Ordinal));
                    internalItem.isAvailable = true;
                    item.objectType = StaticItems.Types.Static;
                }
                else
                {
                    Console.WriteLine("I cant use this item");
                }
            }
            else
            {
                Console.WriteLine("Couldn't use that.");
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

        private void ThreeItemPickup(string[] commands)
        {

            StaticItems? item = currentRoom.StaticItems.Find(i => i.Name.Equals(commands[2], StringComparison.CurrentCultureIgnoreCase));


            if (item != null && item.objectType == StaticItems.Types.Pickup && item.isAvailable != false)
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

        private void FourUseOn(string[] commands)
        {
            if (playerInventory.Count == 0)
            {
                Console.WriteLine("There's nothing in my inventory");
                return;
            }
            

            StaticItems useItem = playerInventory.Find(i => i.Name.Equals(commands[1], StringComparison.CurrentCultureIgnoreCase));
            StaticItems interactItem = currentRoom.StaticItems.Find(i => i.Name.Equals(commands[3], StringComparison.CurrentCultureIgnoreCase));
            if (useItem == null)
            {
                Console.WriteLine("I dont have that item");
                return;
            }
            if (interactItem == null)
            {
                Console.WriteLine("That item isnt in the room.");
                return;
            }
            if (useItem.objectType != StaticItems.Types.Pickup && interactItem.objectType != StaticItems.Types.Interaction)
            {
                Console.WriteLine("Theres nowhere to use this on that");
                return;
            }
            if (useItem.id != interactItem.id)
            {
                Console.WriteLine("I Could not use " + useItem.Name + " on " + interactItem.Name);
            }
            else
            {
                playerInventory.Remove(useItem);
                interactItem.state = 1;
                Console.WriteLine(interactItem.Description[1]);
            }
        }
    }
}
