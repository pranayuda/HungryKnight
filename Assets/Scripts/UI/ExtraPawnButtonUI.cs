using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ExtraPawnButtonUI : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text labelPawn;
    [SerializeField] private TMP_Text labelBoard;

    void Update()
    {
        if (GameManager.Instance.State != GameState.Playing)
        {
            button.interactable = false;
            return;
        }

        if (GameManager.Instance.IsGameOver)
        {
            button.interactable = false;
            return;
        }

        bool hasExtraPawn =
            ExtraPawnManager.Instance.ExtraPawnCount > 0;

        bool hasCaptureMove =
            KnightMoveLogic.HasAnyCaptureMove(
                BoardController.Instance.KnightPosition,
                BoardController.Instance.BoardWidth,
                BoardController.Instance.BoardHeight,
                BoardController.Instance.HasPawnAt
            );

        button.interactable = hasExtraPawn && !hasCaptureMove;

        labelPawn.text = "Extra Pawns: " + ExtraPawnManager.Instance.ExtraPawnCount.ToString();
        labelBoard.text = "Level: " + (LevelManager.Instance.ClearedBoards + 1).ToString();
    }

    public void OnClick()
    {
        if (GameManager.Instance.State != GameState.Playing)
            return;

        ExtraPawnManager.Instance.StartPlacingPawn();
        BoardController.Instance.ShowEmptySquaresForPawn();
    }
}