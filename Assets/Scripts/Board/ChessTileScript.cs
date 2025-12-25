using UnityEngine;

public class ChessTileScript : MonoBehaviour
{
    public Vector2Int gridPosition;

    // Reference to the move indicator GameObject
    // Indicated when the tile is a valid move destination
    [SerializeField] private GameObject moveIndicator;

    // Initialize the tile with its grid position and color
    public void Init(Vector2Int pos, Color color)
    {
        gridPosition = pos;
        GetComponent<SpriteRenderer>().color = color;
        HideIndicator();
    }

    // Show the move indicator on this tile
    // Called by BoardController when displaying valid moves
    public void ShowIndicator()
    {
        moveIndicator.SetActive(true);
    }

    // Hide the move indicator on this tile
    // Called by BoardController when clearing valid moves
    public void HideIndicator()
    {
        moveIndicator.SetActive(false);
    }

    // Handle mouse click events on the tile
    // Uses old Unity OnMouseDown for simplicity
    void OnMouseDown()
    {
        BoardController.Instance.OnTileClicked(this);
    }
}