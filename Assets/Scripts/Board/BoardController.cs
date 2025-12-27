using UnityEngine;
using System.Collections.Generic;
using System.Numerics;

// Manages the chessboard, pieces, and player interactions on the board.
public class BoardController : MonoBehaviour
{
    // Uses the singleton pattern for easier global access.
    public static BoardController Instance { get; private set; }

    [SerializeField] private BoardRules boardRules;
    [SerializeField] private BoardView boardView;
    [SerializeField] private PieceSpawner pieceSpawner;

    ChessTileScript[,] tiles; // 2D array representing the chessboard tiles
    KnightController knight;

    // Logic handler for pawns on the board, separating game logic from visual representation
    PawnLogic pawnLogic;
    KnightMoveLogic knightMoveLogic;
    BoardHighlightManager highlight;

    bool knightSelected;

    public Vector2Int KnightPosition => knight.GridPosition;
    public int BoardWidth => tiles.GetLength(0);
    public int BoardHeight => tiles.GetLength(1);

    void Awake()
    {
        Instance = this;
    }

    // Generates a new level with the specified board size and enemy count
    public void GenerateLevel(int boardSize, int enemyCount)
    {
        pawnLogic?.Clear();

        // Generate the board by setting up tiles and pieces
        BoardRuntimeData data = new()
        {
            width = boardSize,
            height = boardSize,
            tileSize = boardRules.tileSize,
            lightTileColor = boardRules.lightTileColor,
            darkTileColor = boardRules.darkTileColor
        };

        tiles = boardView.Generate(data);
        highlight = new BoardHighlightManager(tiles);

        // Generate puzzle layout and place pieces following the rules
        PuzzleGenerator generator =
            new(boardSize, boardSize, enemyCount);

        bool success = generator.TryGeneratePuzzle(
            out Vector2Int knightPos,
            out var pawnPositions,
            out _
        );

        if (!success)
            return;

        // Spawn pieces on the board
        knight = pieceSpawner.SpawnKnightAt(
            boardView.transform,
            knightPos,
            data.tileSize,
            boardView.BoardOffset
        );

        var spawnedPawns = pieceSpawner.SpawnPawnsAt(
            boardView.transform,
            pawnPositions,
            data.tileSize,
            boardView.BoardOffset
        );

        // Initialize game logic for pawns and knight movement
        pawnLogic = new PawnLogic();
        pawnLogic.SetInitialPawns(spawnedPawns);

        knightMoveLogic = new KnightMoveLogic(pawnLogic);
    }

    // Handles the event when the knight piece is clicked by the player
    public void OnKnightClicked()
    {
        if (GameManager.Instance.State != GameState.Playing)
            return;

        knightSelected = true;
        highlight.HighlightKnightMoves(knight.GridPosition); // Highlight possible moves
    }

    public bool HasPawnAt(Vector2Int pos)
    {
        return pawnLogic.GetPawnAt(pos) != null;
    }

    // Handles the event when a tile on the board is clicked by the player
    public void OnTileClicked(ChessTileScript tile)
    {
        if (GameManager.Instance.State != GameState.Playing)
            return;

        Vector2Int pos = tile.gridPosition;

        // Handle placing extra pawns if in that mode
        if (ExtraPawnManager.Instance.IsPlacingPawn)
        {
            if (!pawnLogic.IsOccupied(pos, knight))
            {
                PawnController pawn =
                    ExtraPawnManager.Instance.PlacePawn(
                        boardView.transform,
                        pos,
                        boardRules.tileSize,
                        boardView.BoardOffset
                    );

                pawnLogic.AddPawn(pawn);
            }

            highlight.Clear();
            return;
        }

        // Handle knight movement if a knight is selected
        if (!knightSelected)
            return;

        knightMoveLogic.ResolveMove(
            knight,
            pos,
            tiles.GetLength(0),
            tiles.GetLength(1)
        );

        // After moving, reset selection and highlights
        knightSelected = false;
        highlight.Clear();

        // Check if all pawns have been cleared to trigger level completion
        if (!pawnLogic.HasRemainingPawns())
            LevelManager.Instance.OnLevelCleared();
    }

    // Highlights empty squares for placing an extra pawn
    public void ShowEmptySquaresForPawn()
    {
        highlight.HighlightEmptySquares(pawnLogic, knight);
    }
}