using UnityEngine;

public class BoardView : MonoBehaviour
{
    // Prefab for the chess tile
    [SerializeField] private GameObject tilePrefab;

    // Offset to center the board in the scene
    public Vector2 BoardOffset { get; private set; }

    // Generate the board based on runtime data
    public ChessTileScript[,] Generate(BoardRuntimeData data)
    {
        ClearBoard();

        // Create a 2D array to hold the tiles and instantiate them
        ChessTileScript[,] tiles =
            new ChessTileScript[data.width, data.height];

        BoardOffset = GetBoardOffset(data);

        // Loop through width and height to create tiles
        for (int x = 0; x < data.width; x++)
        {
            for (int y = 0; y < data.height; y++)
            {
                // Calculate grid position and world position
                Vector2Int gridPos = new Vector2Int(x, y);

                // Calculate world position with offset to center the board
                Vector3 worldPos =
                    new Vector3(
                        x * data.tileSize,
                        y * data.tileSize,
                        0
                    ) + (Vector3)BoardOffset;

                // Instantiate the tile prefab at the calculated position
                // and set its parent to this BoardView for organization
                GameObject tileGO = Instantiate(
                    tilePrefab,
                    worldPos,
                    Quaternion.identity,
                    transform
                );

                // Initialize the tile script with its grid position and color
                ChessTileScript tile =
                    tileGO.GetComponent<ChessTileScript>();

                // Determine tile color based on position for checkerboard pattern
                bool isLight = (x + y) % 2 == 0;
                Color color = isLight
                    ? data.lightTileColor
                    : data.darkTileColor;

                // Initialize the tile with position and color
                tile.Init(gridPos, color);
                tiles[x, y] = tile;
            }
        }

        return tiles;
    }

    // Calculate the board offset to center it in the scene
    Vector2 GetBoardOffset(BoardRuntimeData data)
    {
        float offsetX = -(data.width - 1) * data.tileSize / 2f;
        float offsetY = -(data.height - 1) * data.tileSize / 2f;
        return new Vector2(offsetX, offsetY);
    }

    // Clear existing tiles from the board
    void ClearBoard()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}