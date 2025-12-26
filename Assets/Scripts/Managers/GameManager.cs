using UnityEngine;

// Manages the overall game state and flow
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState State { get; private set; } = GameState.Idle;
    [SerializeField] private LevelManager levelManager;
    public string GameOverReason { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        BoardController.Instance.GenerateLevel(8,5);
    }

    public bool IsGameOver => State == GameState.GameOver;

    public void StartGame()
    {
        if (State != GameState.Idle)
            return;

        State = GameState.Playing;
        ExtraPawnManager.Instance.ResetExtraPawns();
        ChessTimer.Instance.ResetTimer();

        levelManager.StartFirstLevel();
    }

    public void RestartGame()
    {
        State = GameState.Playing;
        ExtraPawnManager.Instance.ResetExtraPawns();
        ChessTimer.Instance.ResetTimer();
        levelManager.ResetProgression();
        levelManager.StartFirstLevel();
    }

    public void GameOver(string reason)
    {
        if (State == GameState.GameOver)
            return;

        State = GameState.GameOver;
        Debug.Log($"GAME OVER: {reason}");
    }

    public void OnTimeUp()
    {
        GameOverReason = "Time's Up! Be Quicker Next Time!";
        GameOver(GameOverReason);
    }

    public void OnInvalidMove()
    {
        GameOverReason = "Knight Moved to an Empty Square!";
        GameOver(GameOverReason);
    }

    public void OnDeadlockWithoutExtraPawns()
    {
        GameOverReason = "Can't Capture and No Extra Pawns Left!";
        GameOver(GameOverReason);
    }
}