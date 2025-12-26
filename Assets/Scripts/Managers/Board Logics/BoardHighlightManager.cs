using UnityEngine;

public class BoardHighlightManager
{
    private ChessTileScript[,] tiles;

    public BoardHighlightManager(ChessTileScript[,] tiles)
    {
        this.tiles = tiles;
    }

    public void Clear()
    {
        for (int x = 0; x < tiles.GetLength(0); x++)
            for (int y = 0; y < tiles.GetLength(1); y++)
                tiles[x, y].HideIndicator();
    }

    public void HighlightKnightMoves(Vector2Int knightPos)
    {
        Clear();

        foreach (var move in KnightMoveRules.Moves)
        {
            Vector2Int target = knightPos + move;

            if (IsInside(target))
                tiles[target.x, target.y].ShowIndicator();
        }
    }

    public void HighlightEmptySquares(PawnLogic pawnLogic, KnightController knight)
    {
        Clear();

        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                Vector2Int pos = new(x, y);
                if (!pawnLogic.IsOccupied(pos, knight))
                    tiles[x, y].ShowIndicator();
            }
        }
    }

    private bool IsInside(Vector2Int pos)
    {
        return pos.x >= 0 && pos.y >= 0 &&
               pos.x < tiles.GetLength(0) &&
               pos.y < tiles.GetLength(1);
    }
}