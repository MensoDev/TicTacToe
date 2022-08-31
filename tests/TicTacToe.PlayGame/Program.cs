using TicTacToe.PlayGame;

Console.WriteLine("Hello, World!");

var engine = new GameEngine();
var command = "";
while (command is "yes" or "YES" or "Y" or "y" or "" or null)
{
    engine.ResetGame();
    engine.Run();
    Console.Write("\nInsert Y, Yes or Enter for reset game or another key for exit: ");
    command = Console.ReadLine() ?? "yes";
}