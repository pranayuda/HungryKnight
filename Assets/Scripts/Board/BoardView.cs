using UnityEngine;

public class BoardView : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;

    public Vector2 BoardOffset { get; private set; }

    public ChessTileScript[,] Generate(BoardRuntimeData data)
    {
        ClearBoard();

        ChessTileScript[,] tiles =
            new ChessTileScript[data.width, data.height];

        BoardOffset = GetBoardOffset(data);

        for (int x = 0; x < data.width; x++)
        {
            for (int y = 0; y < data.height; y++)
            {
                Vector2Int gridPos = new Vector2Int(x, y);

                Vector3 worldPos =
                    new Vector3(
                        x * data.tileSize,
                        y * data.tileSize,
                        0
                    ) + (Vector3)BoardOffset;

                GameObject tileGO = Instantiate(
                    tilePrefab,
                    worldPos,
                    Quaternion.identity,
                    transform
                );

                ChessTileScript tile =
                    tileGO.GetComponent<ChessTileScript>();

                bool isLight = (x + y) % 2 == 0;
                Color color = isLight
                    ? data.lightTileColor
                    : data.darkTileColor;

                tile.Init(gridPos, color);
                tiles[x, y] = tile;
            }
        }

        return tiles;
    }

    Vector2 GetBoardOffset(BoardRuntimeData data)
    {
        float offsetX = -(data.width - 1) * data.tileSize / 2f;
        float offsetY = -(data.height - 1) * data.tileSize / 2f;
        return new Vector2(offsetX, offsetY);
    }

    void ClearBoard()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}