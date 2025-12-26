using UnityEngine;

// Static class defining the movement rules for a knight piece
public static class KnightMoveRules
{
    // All possible knight moves represented as (x, y) offsets
    public static readonly Vector2Int[] Moves =
    {
        // these represent the 8 possible L-shaped moves by calculating the change in x and y coordinates
        new Vector2Int(1, 2),
        new Vector2Int(2, 1),
        new Vector2Int(-1, 2),
        new Vector2Int(-2, 1),
        new Vector2Int(1, -2),
        new Vector2Int(2, -1),
        new Vector2Int(-1, -2),
        new Vector2Int(-2, -1)
    };

    // Check if a move from 'from' to 'to' is valid for a knight
    public static bool IsValidMove(Vector2Int from, Vector2Int to)
    {
        Vector2Int delta = to - from;

        foreach (var move in Moves)
        {
            if (delta == move)
                return true;
        }
        return false;
    }
}