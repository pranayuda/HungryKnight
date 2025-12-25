using UnityEngine;

public class GameUIController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject gameOverPanel;

    void Start()
    {
        RefreshUI();
    }

    void Update()
    {
        RefreshUI();
    }

    void RefreshUI()
    {
        switch (GameManager.Instance.State)
        {
            case GameState.Idle:
                startButton.SetActive(true);
                gameOverPanel.SetActive(false);
                break;

            case GameState.Playing:
                startButton.SetActive(false);
                gameOverPanel.SetActive(false);
                break;

            case GameState.GameOver:
                startButton.SetActive(false);
                gameOverPanel.SetActive(true);
                break;
        }
    }

    public void OnStartClicked()
    {
        GameManager.Instance.StartGame();
    }

    public void OnRestartClicked()
    {
        GameManager.Instance.RestartGame();
    }
}