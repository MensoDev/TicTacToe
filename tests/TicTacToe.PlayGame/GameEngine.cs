using TicTacToe.Algorithms;
using TicTacToe.Enums;

namespace TicTacToe.PlayGame;

using static Console;

public sealed class GameEngine
{
    public GameEngine()
    {
        GamePlay = new Game();
    }

    private Game GamePlay { get; set; }

    public void Run()
    {
        while (GamePlay.State is GameState.WaitingAiPlayer or GameState.WaitingHumanPlayer)
        {
            Clear();
            RenderGame();
            
            switch (GamePlay.State)
            {
                case GameState.WaitingAiPlayer:
                {
                    var position = MinimaxAlgorithm.FindBestPosition(GamePlay.Clone());
                    GamePlay.Move(position, Game.AiToken);
                
                    Clear();
                    RenderGame();
                    break;
                }
                case GameState.WaitingHumanPlayer:
                {
                    var position = RequestPosition();
                    GamePlay.Move(position, Game.HumanToken);
                
                    Clear();
                    RenderGame();
                    break;
                }
                
                case GameState.HumanPlayerWon:
                case GameState.AiPlayerWon:
                case GameState.TiedGame:
                default: throw new ArgumentOutOfRangeException();
            }
        }
        
        Clear();
        RenderGame();

        switch (GamePlay.State)
        {
            case GameState.TiedGame : WriteLine("O jogo terminou em empate...."); break;
            case GameState.AiPlayerWon : WriteLine("O jogador IA ganhou"); break;
            case GameState.HumanPlayerWon : WriteLine("O jogador humano ganhou...."); break;
            case GameState.WaitingHumanPlayer:
            case GameState.WaitingAiPlayer: WriteLine("Ops! algo deve ter dado erado...");  break;
            default: WriteLine("Ops! este estado do jogo não esta mapeado..."); break;
        }

    }

    public void ResetGame()
    {
        GamePlay = new Game();
    }

    private void RenderGame()
    {
        var p1G = MapperToken(GamePlay.Board[0, 0]);
        var p2G = MapperToken(GamePlay.Board[0, 1]);
        var p3G = MapperToken(GamePlay.Board[0, 2]);
        var p4G = MapperToken(GamePlay.Board[1, 0]);
        var p5G = MapperToken(GamePlay.Board[1, 1]);
        var p6G = MapperToken(GamePlay.Board[1, 2]);
        var p7G = MapperToken(GamePlay.Board[2, 0]);
        var p8G = MapperToken(GamePlay.Board[2, 1]);
        var p9G = MapperToken(GamePlay.Board[2, 2]);

        WriteLine($""" 
        +-----------------------------------------------------------------------------------------+
        |-------------------- Tic Tac Toe Game ------------------ By MensoDev --------------------|
        |----------------------------------------------------------------------- v.0.0.0-alpha5 --|
        +---------------------------------------------|-------------------------------------------+
        |                                             |                                           |
        |                {p1G} | {p2G} | {p3G}                    |               a1 | a2 | a3                |
        |               ---|---|---                   |              ____|____|____               |
        |                {p4G} | {p5G} | {p6G}                    |               b1 | b2 | b3                |
        |               ---|---|---                   |              ____|____|____               |
        |                {p7G} | {p8G} | {p9G}                    |               c1 | c2 | c3                |
        |                                             |                                           |
        +-----------------------------------------------------------------------------------------+

        """
        );
    }

    private static string MapperToken(char token)
    {
        return token switch
        {
            Game.AiToken => "X",
            Game.HumanToken => "O",
            _ => " "
        };
    }

    private static Position RequestPosition()
    {
        var valid = false;
        while (valid is false)
        {
            Write("\nInforme a posição escolhida: ");
            var token = ReadLine();
            var position = ConvertToLiteralPosition(token ?? "");

            if (position is null)
            {
                valid = false;
            }
            else
            {
                return position.Value;
            }
        }

        return new Position();
    }

    private static Position? ConvertToLiteralPosition(string token)
    {
        Position? position = token switch
        {
            "a1" => new Position(0, 0),
            "a2" => new Position(0, 1),
            "a3" => new Position(0, 2),
            "b1" => new Position(1, 0),
            "b2" => new Position(1, 1),
            "b3" => new Position(1, 2),
            "c1" => new Position(2, 0),
            "c2" => new Position(2, 1),
            "c3" => new Position(2, 2),
            _ => null
        };

        if (position is not null) return position;

        WriteLine("Commando Invalido:::");
        return position;
    }
}