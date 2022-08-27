// See https://aka.ms/new-console-template for more information

using TicTacToe;
using TicTacToe.Algorithms;
using TicTacToe.Enums;
using TicTacToe.PlayGame;

Console.WriteLine("Hello, World!");

var engine = new GameEngine();
var command = "";
while (command is "yes" or "YES" or "Y" or "y" or "" or null)
{
    engine.ResetGame();
    engine.Run();
    Console.Write("\nDigite Y, Yes or Enter for reset game or another key for exit: ");
    command = Console.ReadLine() ?? "yes";
}

// var game = new Game();
//
// var winner = game.CheckWinner();
// var isAi = true;
//
// while (winner is WinnerResult.GameNotFinished)
// {
//     RenderBoard();
//     if (isAi)
//     {
//         var position = MinimaxAlgorithm.FindBestPosition(game.Clone());
//         game.Move(position, Game.AiToken);
//         isAi = false;
//         winner = game.CheckWinner();
//         RenderBoard();
//     }
//     else
//     {
//         Console.Write("\nRow ID: ");
//         var rowPositionId = Convert.ToInt32(Console.ReadLine());
//
//         Console.Write("\nRow ID: ");
//         var colPositionId = Convert.ToInt32(Console.ReadLine());
//
//         game.Move(new Position(rowPositionId, colPositionId), Game.HumanToken);
//         isAi = true;
//         winner = game.CheckWinner();
//         RenderBoard();
//     }
// }
//
// void RenderBoard()
// {
//     Console.Clear();
//     for (var row = 0; row < 3; row++)
//     {
//         for (var col = 0; col < 3; col++)
//         {
//             Console.Write(Map(game.Board[row, col]));
//             Console.Write(col is 2 ? "" : "|");
//         }
//         Console.WriteLine("\n----------");
//     }
// }
//
// string Map(char token)
// {
//     return token switch
//     {
//         Game.AiToken => " X ",
//         Game.HumanToken => " O ",
//         _ => "   "
//     };
// }