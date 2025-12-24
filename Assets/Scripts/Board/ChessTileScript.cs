using UnityEngine;

public class ChessTileScript : MonoBehaviour
{
    public Vector2Int GridPosition { get; private set; }

    public void Init(Vector2Int gridPos, Color color)
    {
        GridPosition = gridPos;
        GetComponent<SpriteRenderer>().color = color;
    }
}
