using UnityEngine;

public class ChessTileScript : MonoBehaviour
{
    public Vector2Int gridPosition;

    [SerializeField] private GameObject moveIndicator;

    public void Init(Vector2Int pos, Color color)
    {
        gridPosition = pos;
        GetComponent<SpriteRenderer>().color = color;
        HideIndicator();
    }

    public void ShowIndicator()
    {
        moveIndicator.SetActive(true);
    }

    public void HideIndicator()
    {
        moveIndicator.SetActive(false);
    }

    void OnMouseDown()
    {
        BoardController.Instance.OnTileClicked(this);
    }
}