using UnityEngine;

public class KnightController : MonoBehaviour
{
    public Vector2Int GridPosition { get; private set; }

    float tileSize;
    Vector2 boardOffset;

    Vector2Int[] knightMoves =
    {
        new Vector2Int(1, 2),
        new Vector2Int(2, 1),
        new Vector2Int(-1, 2),
        new Vector2Int(-2, 1),
        new Vector2Int(1, -2),
        new Vector2Int(2, -1),
        new Vector2Int(-1, -2),
        new Vector2Int(-2, -1)
    };

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

    public bool IsValidKnightMove(Vector2Int target)
    {
        Vector2Int delta = target - GridPosition;

        foreach (var move in knightMoves)
        {
            if (delta == move)
                return true;
        }
        return false;
    }

    public bool TryMove(
        Vector2Int target,
        int boardWidth,
        int boardHeight
    )
    {
        if (!IsValidKnightMove(target))
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