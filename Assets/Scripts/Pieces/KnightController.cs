using UnityEngine;

public class KnightController : MonoBehaviour
{
    public Vector2Int GridPosition { get; private set; }
    float tileSize;
    Vector2 boardOffset;

    // Initialize the knight's position and board parameters
    // Called by PieceSpawner when spawning the knight
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

    // Set the knight's position on the board
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

    // Attempt to move the knight to the target position
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

    // Handle mouse click events on the knight
    // Uses old Unity OnMouseDown for simplicity
    void OnMouseDown()
    {
        // uses singleton pattern to notify BoardController because it is easier
        BoardController.Instance.OnKnightClicked();
    }
}