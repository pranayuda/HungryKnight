using System.Collections.Generic;
using UnityEngine;

// Class responsible for generating puzzles on the chessboard
public class PuzzleGenerator
{
    int boardWidth;
    int boardHeight;
    int enemyCount;
    int maxRetry;
    List<Vector2Int> solutionPath;

    public PuzzleGenerator(
        int boardWidth,
        int boardHeight,
        int enemyCount,
        int maxRetry = 1000
    )
    {
        this.boardWidth = boardWidth;
        this.boardHeight = boardHeight;
        this.enemyCount = enemyCount;
        this.maxRetry = maxRetry;
    }

    // Attempts to generate a solvable puzzle configuration
    public bool TryGeneratePuzzle(
        out Vector2Int knightStartPos,
        out List<Vector2Int> pawnPositions,
        out List<Vector2Int> solutionPath
    )
    {
        for (int attempt = 0; attempt < maxRetry; attempt++)
        {
            if (TryGeneratePath(
                out knightStartPos,
                out pawnPositions,
                out solutionPath
            ))
            {
                return true;
            }
        }

        knightStartPos = Vector2Int.zero;
        pawnPositions = null;
        solutionPath = null;
        return false;
    }

    // Generates a path for the knight and places pawns accordingly
    bool TryGeneratePath(
        out Vector2Int knightStartPos,
        out List<Vector2Int> pawnPositions,
        out List<Vector2Int> solutionPath
    )
    {
        // Initialize outputs and occupied positions
        pawnPositions = new List<Vector2Int>();
        solutionPath = new List<Vector2Int>();
        HashSet<Vector2Int> occupied = new HashSet<Vector2Int>();

        Vector2Int current = GetRandomPosition();
        occupied.Add(current);

        solutionPath.Add(current);

        // Build the path in reverse, placing pawns along the way
        // This ensures the knight can reach all pawns
        for (int i = 0; i < enemyCount; i++)
        {
            List<Vector2Int> candidates =
                GetValidReverseMoves(current, occupied);

            if (candidates.Count == 0)
            {
                knightStartPos = Vector2Int.zero;
                pawnPositions = null;
                solutionPath = null;
                return false;
            }

            Vector2Int next =
                candidates[Random.Range(0, candidates.Count)];

            pawnPositions.Add(current);
            occupied.Add(next);

            current = next;
            solutionPath.Add(current);
        }

        // Reverse the solution path to get the correct order
        solutionPath.Reverse();
        knightStartPos = solutionPath[0];

        return true;
    }

    // Gets valid reverse moves for the knight from a given position
    List<Vector2Int> GetValidReverseMoves(
        Vector2Int from,
        HashSet<Vector2Int> occupied
    )
    {
        List<Vector2Int> results = new List<Vector2Int>();

        foreach (var move in KnightMoveRules.Moves)
        {
            Vector2Int candidate = from + move;

            if (!IsInsideBoard(candidate))
                continue;

            if (occupied.Contains(candidate))
                continue;

            results.Add(candidate);
        }

        return results;
    }

    Vector2Int GetRandomPosition()
    {
        int x = Random.Range(0, boardWidth);
        int y = Random.Range(0, boardHeight);
        return new Vector2Int(x, y);
    }

    bool IsInsideBoard(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < boardWidth &&
               pos.y >= 0 && pos.y < boardHeight;
    }
}