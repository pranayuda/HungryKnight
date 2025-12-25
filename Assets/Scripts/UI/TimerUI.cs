using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private ChessTimer chessTimer;
    [SerializeField] private TMP_Text timerText;

    void Update()
    {
        if (chessTimer == null)
            return;

        UpdateText(chessTimer.TimeRemaining);
    }

    void UpdateText(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);

        timerText.text =
            $"{minutes:00}:{seconds:00}";
    }
}