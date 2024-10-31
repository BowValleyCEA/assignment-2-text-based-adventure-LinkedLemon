// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using game1402_a2_starter;

Console.WriteLine("Welcome! Type help if you need the commands");
string fileName = "../../../game_data.json";//if you are ever worried about whether your json is valid or not, check out JSON Lint: 

GameData yourGameData = new GameData();
string jsonString = File.ReadAllText(@fileName);
yourGameData = JsonSerializer.Deserialize<GameData>(jsonString);
Game yourGame = new Game(yourGameData);

yourGame.currentRoom = yourGame._gameData.Rooms[0];

Console.WriteLine(yourGameData.GameName);
Console.WriteLine(yourGameData.Description);

while (true)
{
    yourGame.ProcessString(Console.ReadLine());
}
