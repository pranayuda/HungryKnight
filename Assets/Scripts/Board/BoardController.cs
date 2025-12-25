using UnityEngine;
using System.Collections.Generic;

public class BoardController : MonoBehaviour
{
    public static BoardController Instance { get; private set; }

    [Header("Rules")]
    [SerializeField] private BoardRules boardRules;


    [Header("References")]
    [SerializeField] private BoardView boardView;
    [SerializeField] private PieceSpawner pieceSpawner;
    [SerializeField] private LevelManager levelManager;

    ChessTileScript[,] tiles;
    KnightController knight;
    List<PawnController> pawns;
    bool knightSelected = false;

    void Awake()
    {
        Instance = this;
    }

    // Generate a new level with initial board size
    public void GenerateLevel(int boardSize, int enemyCount)
    {
        ClearBoardState();

        int size = boardSize;
        
        BoardRuntimeData data = new BoardRuntimeData
        {
            width = size,
            height = size,
            tileSize = boardRules.tileSize,
            lightTileColor = boardRules.lightTileColor,
            darkTileColor = boardRules.darkTileColor
        };

        tiles = boardView.Generate(data);

        PuzzleGenerator generator =
            new PuzzleGenerator(
                data.width,
                data.height,
                enemyCount
            );

        bool success = generator.TryGeneratePuzzle(
            out Vector2Int knightPos,
            out List<Vector2Int> pawnPositions,
            out List<Vector2Int> solutionPath
        );

        if (!success)
        {
            Debug.LogError("Failed to generate puzzle");
            return;
        }


        Debug.Log("PUZZLE SOLUTION PATH");
        for (int i = 0; i < solutionPath.Count; i++)
        {
            Debug.Log($"Step {i}: {solutionPath[i]}");
        }
        Debug.Log($"Knight Position: {knightPos}");

        knight = pieceSpawner.SpawnKnightAt(
            boardView.transform,
            knightPos,
            data.tileSize,
            boardView.BoardOffset
        );

        pawns = pieceSpawner.SpawnPawnsAt(
            boardView.transform,
            pawnPositions,
            data.tileSize,
            boardView.BoardOffset
        );
    }
    
    // Called when the knight is clicked
    // Included here because BoardController manages everything on the board
    public void OnKnightClicked()
    {
        knightSelected = true;
        ShowValidMoves();
    }

    PawnController GetPawnAt(Vector2Int pos)
    {
        foreach (var pawn in pawns)
        {
            if (pawn.GridPosition == pos)
                return pawn;
        }
        return null;
    }

    // Called when a tile is clicked
    public void OnTileClicked(ChessTileScript tile)
    {
        if (!knightSelected)
            return;
        // Attempt to move the knight to the clicked tile
        Vector2Int target = tile.gridPosition;

        bool moved = knight.TryMove(
            target,
            tiles.GetLength(0),
            tiles.GetLength(1)
        );

        // If moved, check for pawn capture
        if (moved)
        {
            PawnController pawn = GetPawnAt(target);

            if (pawn != null)
            {
                pawns.Remove(pawn);
                Destroy(pawn.gameObject);

                CheckLevelClear();
            }

            knightSelected = false;
            ClearIndicators();
            return;
        }

        // If move failed, deselect the knight
        knightSelected = false;
        ClearIndicators();
    }

    // Show valid moves for the knight by highlighting tiles
    // Called when the knight is clicked
    void ShowValidMoves()
    {
        ClearIndicators();

        Vector2Int pos = knight.GridPosition;

        foreach (var move in KnightMoveRules.Moves)
        {
            Vector2Int target = pos + move;

            if (IsInsideBoard(target))
            {
                tiles[target.x, target.y].ShowIndicator();
            }
        }
    }

    // Clear all move indicators from the tiles
    void ClearIndicators()
    {
        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                tiles[x, y].HideIndicator();
            }
        }
    }
    
    // Check if a position is inside the board boundaries
    bool IsInsideBoard(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < tiles.GetLength(0) &&
               pos.y >= 0 && pos.y < tiles.GetLength(1);
    }
    // Clear the current board state before generating a new level
    void ClearBoardState()
    {
        knightSelected = false;

        if (knight != null)
            Destroy(knight.gameObject);

        if (pawns != null)
        {
            foreach (var pawn in pawns)
            {
                Destroy(pawn.gameObject);
            }
            pawns.Clear();
        }
    }

    void CheckLevelClear()
    {
        if (pawns.Count > 0)
            return;

        Debug.Log("LEVEL CLEARED!");

        levelManager.OnLevelCleared();
    }
}