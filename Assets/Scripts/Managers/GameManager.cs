using UnityEngine;

// Manages the overall game state and flow
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    // Current state of the game, whether idle, playing, or game over
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
    }

    private void Start() {
         // This is just for visual aesthetics in the scene.
        BoardController.Instance.GenerateLevel(8, 8);
    }

    public bool IsGameOver => State == GameState.GameOver;

    // Starts the game from the idle state, resetting necessary components
    public void StartGame()
    {
        if (State != GameState.Idle)
            return;

        State = GameState.Playing;
        ExtraPawnManager.Instance.ResetExtraPawns();
        ChessTimer.Instance.ResetTimer();

        levelManager.StartFirstLevel();
    }

    // Restarts the game, resetting all progress and states
    public void RestartGame()
    {
        State = GameState.Playing;
        ExtraPawnManager.Instance.ResetExtraPawns();
        ChessTimer.Instance.ResetTimer();
        levelManager.ResetProgression();
        levelManager.StartFirstLevel();
    }

    // Handles transitioning to the game over state with a reason
    public void GameOver(string reason)
    {
        if (State == GameState.GameOver)
            return;

        State = GameState.GameOver;
        Debug.Log($"GAME OVER: {reason}");
    }

    // Handles every game over scenario with appropriate messages
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