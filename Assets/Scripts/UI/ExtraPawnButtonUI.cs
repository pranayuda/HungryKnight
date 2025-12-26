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
        ExtraPawnManager.Instance.StartPlacingPawn();
    }
}