using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private GameObject toggleRestartButton;
    [SerializeField] private GameObject toggleStartButton;
    [SerializeField] private GameObject gameOverPanel;
    private string lastState = "";
    private string currentState = "";

    void Awake()
    {
        RefreshUI();
    }

    void Update()
    {
        currentState = GameManager.Instance.State.ToString();
        if (currentState != lastState)
        {
            lastState = currentState;
            RefreshUI();
        }
    }

    void RefreshUI()
    {
        switch (GameManager.Instance.State)
        {
            case GameState.Idle:
                startButton.interactable = true;
                toggleRestartButton.SetActive(false);
                gameOverPanel.SetActive(false);
                break;

            case GameState.Playing:
                startButton.interactable = false;
                restartButton.interactable = false;
                gameOverPanel.SetActive(false);
                break;

            case GameState.GameOver:
                toggleStartButton.SetActive(false);
                toggleRestartButton.SetActive(true);
                gameOverPanel.SetActive(true);
                restartButton.interactable = true;
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

    public void OnClosePanelClicked()
    {
        gameOverPanel.SetActive(false);
    }
}