using UnityEngine;

public class KnightController : MonoBehaviour
{
    public Vector2Int GridPosition { get; private set; }

    float tileSize;
    Vector2 boardOffset;

    public void Init(
        Vector2Int startPos,
        float tileSize,
        Vector2 boardOffset
    )
    {
        this.tileSize = tileSize;
        this.boardOffset = boardOffset;
        SetPosition(startPos);
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

    public bool TryMove(
        Vector2Int target,
        int boardWidth,
        int boardHeight
    )
    {
        if (!KnightMoveRules.IsValidMove(GridPosition, target))
            return false;

        if (target.x < 0 || target.x >= boardWidth ||
            target.y < 0 || target.y >= boardHeight)
            return false;

        SetPosition(target);
        return true;
    }

    void OnMouseDown()
    {
        BoardController.Instance.OnKnightClicked();
    }
}