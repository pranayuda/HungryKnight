using UnityEngine;
using System.Collections.Generic;
using System.Numerics;

public class BoardController : MonoBehaviour
{
    public static BoardController Instance { get; private set; }

    [SerializeField] private BoardRules boardRules;
    [SerializeField] private BoardView boardView;
    [SerializeField] private PieceSpawner pieceSpawner;
    [SerializeField] private LevelManager levelManager;

    ChessTileScript[,] tiles;
    KnightController knight;

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

    public void GenerateLevel(int boardSize, int enemyCount)
    {
        pawnLogic?.Clear();

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

        PuzzleGenerator generator =
            new(boardSize, boardSize, enemyCount);

        bool success = generator.TryGeneratePuzzle(
            out Vector2Int knightPos,
            out var pawnPositions,
            out _
        );

        if (!success)
            return;

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

        pawnLogic = new PawnLogic();
        pawnLogic.SetInitialPawns(spawnedPawns);

        knightMoveLogic = new KnightMoveLogic(pawnLogic);
    }

    public void OnKnightClicked()
    {
        if (GameManager.Instance.IsGameOver)
            return;

        knightSelected = true;
        highlight.HighlightKnightMoves(knight.GridPosition);
    }

    public bool HasPawnAt(Vector2Int pos)
    {
        return pawnLogic.GetPawnAt(pos) != null;
    }

    public void OnTileClicked(ChessTileScript tile)
    {
        if (GameManager.Instance.IsGameOver)
            return;

        Vector2Int pos = tile.gridPosition;

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

        if (!knightSelected)
            return;

        knightMoveLogic.ResolveMove(
            knight,
            pos,
            tiles.GetLength(0),
            tiles.GetLength(1)
        );

        knightSelected = false;
        highlight.Clear();

        if (!pawnLogic.HasRemainingPawns())
            levelManager.OnLevelCleared();
    }

    public void ShowEmptySquaresForPawn()
    {
        highlight.HighlightEmptySquares(pawnLogic, knight);
    }
}