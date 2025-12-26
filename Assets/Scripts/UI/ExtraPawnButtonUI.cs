using UnityEngine;
using TMPro;

public class ExtraPawnButtonUI : MonoBehaviour
{
    [SerializeField] private TMP_Text label;

    void Update()
    {
        int count = ExtraPawnManager.Instance.ExtraPawnCount;
        label.text = $"Extra Pawn ({count})";
    }

    public void OnClick()
    {
        if (GameManager.Instance.State != GameState.Playing)
            return;
        ExtraPawnManager.Instance.StartPlacingPawn();
    }
}