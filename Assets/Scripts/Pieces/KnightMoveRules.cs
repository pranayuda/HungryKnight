using UnityEngine;

public static class KnightMoveRules
{
    public static readonly Vector2Int[] Moves =
    {
        new Vector2Int(1, 2),
        new Vector2Int(2, 1),
        new Vector2Int(-1, 2),
        new Vector2Int(-2, 1),
        new Vector2Int(1, -2),
        new Vector2Int(2, -1),
        new Vector2Int(-1, -2),
        new Vector2Int(-2, -1)
    };

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