using TicTacToe.Enums;

namespace TicTacToe.Algorithms;

public static class MinimaxAlgorithm
{
    public static Position FindBestPosition(Game game)
    {
        var bestScore = int.MinValue;
        var bestPosition = new Position();

        var availablePositions = game.GetAvailablePositions();

        while (availablePositions.TryDequeue(out var position, out _))
        {
            game.Move(position, Game.AiToken);
            var score = Minimax(game.Clone(), 0, false);
            game.UndoMove();

            if (score <= bestScore) continue;
            
            bestScore = score;
            bestPosition = position;
        }

        return bestPosition;
    }

    private static int Minimax(Game game, int depth, bool isMaximizing)
    {
        var winnerResult = game.CheckWinner();

        if (winnerResult is WinnerResult.Tie or WinnerResult.AiPlayer or WinnerResult.HumanPlayer)
        {
            return (int) winnerResult;
        }

        if (isMaximizing)
        {
            var availablePositions = game.GetAvailablePositions();
            var bestScore = int.MinValue;

            while (availablePositions.TryDequeue(out var position, out _))
            {
                game.Move(position, Game.AiToken);
                bestScore = Math.Max(Minimax(game.Clone(), depth++, false), bestScore);
                game.UndoMove();
            }

            return bestScore;
        }
        else
        {
            var availablePositions = game.GetAvailablePositions();
            var bestScore = int.MaxValue;

            while (availablePositions.TryDequeue(out var position, out _))
            {
                game.Move(position, Game.HumanToken);
                bestScore = Math.Min(Minimax(game.Clone(), depth++, true), bestScore);
                game.UndoMove();
            }

            return bestScore;
        }
    }
}