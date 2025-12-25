using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
    [SerializeField] private GameObject knightPrefab;

    public KnightController SpawnKnight(
        Transform parent,
        int boardWidth,
        int boardHeight,
        float tileSize,
        Vector2 boardOffset
    )
    {
        int x = Random.Range(0, boardWidth);
        int y = Random.Range(0, boardHeight);

        GameObject go = Instantiate(knightPrefab, parent);
        KnightController knight = go.GetComponent<KnightController>();

        knight.Init(
            new Vector2Int(x, y),
            tileSize,
            boardOffset
        );

        return knight;
    }
}