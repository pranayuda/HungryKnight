using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState State { get; private set; } = GameState.Idle;
    [SerializeField] private LevelManager levelManager;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
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
        GameOver("TIME UP");
    }

    public void OnInvalidMove()
    {
        GameOver("KNIGHT MOVED TO EMPTY TILE");
    }
}