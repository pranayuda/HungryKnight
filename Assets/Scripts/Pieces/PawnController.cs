using UnityEngine;

public class PawnController : MonoBehaviour
{
    public Vector2Int GridPosition { get; private set; }

    float tileSize;
    Vector2 boardOffset;

    public void Init(
        Vector2Int pos,
        float tileSize,
        Vector2 boardOffset
    )
    {
        this.tileSize = tileSize;
        this.boardOffset = boardOffset;
        SetPosition(pos);
    }

    public void SetPosition(Vector2Int pos)
    {
        GridPosition = pos;

        transform.localPosition =
            new Vector3(
                pos.x * tileSize,
                pos.y * tileSize,
                -1
            ) + (Vector3)boardOffset;
    }
}