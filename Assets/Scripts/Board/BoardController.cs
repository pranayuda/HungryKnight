using UnityEngine;

public class BoardController : MonoBehaviour
{
    public static BoardController Instance { get; private set; }

    [Header("Rules")]
    [SerializeField] private BoardRules boardRules;

    [Header("References")]
    [SerializeField] private BoardView boardView;
    [SerializeField] private PieceSpawner pieceSpawner;

    ChessTileScript[,] tiles;
    KnightController knight;
    bool knightSelected = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        int size = boardRules.minSize;

        BoardRuntimeData data = new BoardRuntimeData
        {
            width = size,
            height = size,
            tileSize = boardRules.tileSize,
            lightTileColor = boardRules.lightTileColor,
            darkTileColor = boardRules.darkTileColor
        };

        tiles = boardView.Generate(data);

        knight = pieceSpawner.SpawnKnight(
            boardView.transform,
            data.width,
            data.height,
            data.tileSize,
            boardView.BoardOffset
        );
    }

    public void OnKnightClicked()
    {
        knightSelected = true;
        ShowValidMoves();
    }

    public void OnTileClicked(ChessTileScript tile)
    {
        if (!knightSelected)
            return;

        bool moved = knight.TryMove(
            tile.gridPosition,
            tiles.GetLength(0),
            tiles.GetLength(1)
        );

        if (moved)
        {
            knightSelected = false;
            ClearIndicators();
            return;
        }

        knightSelected = false;
        ClearIndicators();
    }

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

    bool IsInsideBoard(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < tiles.GetLength(0) &&
               pos.y >= 0 && pos.y < tiles.GetLength(1);
    }
}