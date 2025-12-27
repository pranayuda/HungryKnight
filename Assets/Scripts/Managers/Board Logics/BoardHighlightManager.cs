using UnityEngine;

// Manages highlighting of tiles on the chessboard for possible moves and placements.
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

    // Highlights possible knight moves from the given position
    public void HighlightKnightMoves(Vector2Int knightPos)
    {
        Clear();

        // Iterate through all possible knight moves
        foreach (var move in KnightMoveRules.Moves)
        {
            Vector2Int target = knightPos + move;

            if (IsInside(target))
                tiles[target.x, target.y].ShowIndicator();
        }
    }

    // Highlights empty squares where a pawn can be placed
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

    // Checks if the given position is within the bounds of the board
    private bool IsInside(Vector2Int pos)
    {
        return pos.x >= 0 && pos.y >= 0 &&
               pos.x < tiles.GetLength(0) &&
               pos.y < tiles.GetLength(1);
    }
}