using TicTacToe.Enums;

namespace TicTacToe;

public sealed class Game
{
    private const char EmptyToken = '_';
    public const char HumanToken = 'O';
    public const char AiToken = 'X';
    
    
    public Game()
    {
        Board = new[,]
        {
            {EmptyToken, EmptyToken, EmptyToken},
            {EmptyToken, EmptyToken, EmptyToken},
            {EmptyToken, EmptyToken, EmptyToken}
        };

        LastPosition = null;
        State = GameState.WaitingAiPlayer;
    }
    
    private Game(char[,] board, Position? lastPosition, GameState state )
    {
        Board = board;
        State = state;
        LastPosition = lastPosition;
    }
    
    public char[,] Board { get; set; }
    private Position? LastPosition { get; set; }
    public GameState State { get; private set; } 
    
    public Game Clone()
    {
        var board = Board.Clone() as char[,] ?? throw new InvalidOperationException();
        return new Game(board, LastPosition, State);
    }
    public void Move(Position position, char playerToken)
    {
        Board[position.Row, position.Col] = playerToken;
        LastPosition = position;

        var winner = CheckWinner();

        State = winner switch
        {
            WinnerResult.Tie => GameState.TiedGame,
            WinnerResult.AiPlayer => GameState.AiPlayerWon,
            WinnerResult.HumanPlayer => GameState.HumanPlayerWon,
            WinnerResult.GameNotFinished => playerToken is AiToken ? GameState.WaitingHumanPlayer : GameState.WaitingAiPlayer,
            _ => throw new ArgumentOutOfRangeException($"{winner}")
        };
    }
    public void UndoMove()
    {
        if(LastPosition is null) return;

        Board[LastPosition.Value.Row, LastPosition.Value.Col] = EmptyToken;
        LastPosition = null;
    }
    public PriorityQueue<Position, int> GetAvailablePositions()
    {
        var availablePositions = new PriorityQueue<Position, int>();
        var priority = 0;
        for (var row = 0; row < 3; row++)
        {
            for (var col = 0; col < 3; col++)
            {
                if (Board[row, col] is EmptyToken)
                {
                    availablePositions.Enqueue(new Position(row, col), priority++);
                }
            }
        }
        return availablePositions;
    }

    public WinnerResult CheckWinner()
    {
        if (CheckWinnerInLines(out var winnerPlayerTokenInLine)) return IdentifierToken(winnerPlayerTokenInLine);
        if (CheckWinnerInColumns(out var winnerPlayerTokenInColumn)) return IdentifierToken(winnerPlayerTokenInColumn);
        if (CheckWinnerInDiagonals(out var winnerPlayerTokenInDiagonal)) return IdentifierToken(winnerPlayerTokenInDiagonal);
        
        for (var row = 0; row < 3; row++) 
        {
            for (var col = 0; col < 3; col++) 
            {
                if (Board[row, col] is EmptyToken)
                {
                    return WinnerResult.GameNotFinished;
                }
            }
        }

        return WinnerResult.Tie;
    }
    private static WinnerResult IdentifierToken(char playerToken)
    {
        return playerToken switch
        {
            AiToken => WinnerResult.AiPlayer,
            HumanToken => WinnerResult.HumanPlayer,
            _ => throw new ArgumentOutOfRangeException($"Invalid argument {playerToken}")
        };
    }
    private bool CheckWinnerInLines(out char playerToken)
    {
        for (var row = 0; row < 3; row++)
        {
            if (EqualsThreeAndNotEmpty(Board[row, 0], Board[row, 1], Board[row, 2]) is false) continue;
            
            playerToken = Board[row, 0];
            return true;
        }

        playerToken = EmptyToken;
        return false;
    }
    private bool CheckWinnerInColumns(out char playerToken)
    {
        for (var col = 0; col < 3; col++)
        {
            if (EqualsThreeAndNotEmpty(Board[0, col], Board[1, col], Board[2, col]) is false) continue;
            
            playerToken = Board[0, col];
            return true;
        }

        playerToken = EmptyToken;
        return false;
    }
    private bool CheckWinnerInDiagonals(out char playerToken)
    {

        if (EqualsThreeAndNotEmpty(Board[0, 0], Board[1, 1], Board[2, 2]))
        {
            playerToken = Board[0, 0];
            return true;
        }
        
        if (EqualsThreeAndNotEmpty(Board[0, 2], Board[1, 1], Board[2, 0]))
        {
            playerToken = Board[0, 2];
            return true;
        }

        playerToken = EmptyToken;
        return false;
    }
    
    private static bool EqualsThreeAndNotEmpty(char one, char two, char three)
    {
        return one == two && two == three && three is not EmptyToken;
    }
}



public struct Position
{
    public Position(int row, int col)
    {
        Row = row;
        Col = col;
    }

    public int Row { get; private set; }
    public int Col { get; private set; }
}