using UnityEngine;

// Manages level progression, including board size and enemy count
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    int currentLevel;
    int currentEnemyCount;
    int currentBoardSize;
    public int ClearedBoards => currentLevel - 1;

    [Header("References")]
    [SerializeField] private BoardRules boardRules;

    void Awake() {
        Instance = this;
    }

    void Start()
    {
        ChessTimer.Instance.Init(
            boardRules.baseTimeSeconds,
            boardRules.incrementPerMove
        );

        ResetProgression();
    }

    public void StartFirstLevel()
    {
        StartLevel();
    }
    
    void StartLevel()
    {
        // Generate the level with current parameters
        BoardController.Instance.GenerateLevel(
            currentBoardSize,
            currentEnemyCount
        );
    }


    public void ResetProgression()
    {
        currentLevel = 1;
        currentEnemyCount = boardRules.enemiesToSpawn;
        currentBoardSize = boardRules.minSize;
    }

    // Called when the current level is cleared to progress to the next level
    public void OnLevelCleared()
    {
        if (currentLevel % 5 == 0)
        {
            ExtraPawnManager.Instance.AddExtraPawn(1);
            Debug.Log("Granted an extra pawn for every 5 levels cleared!");
        }

        ChessTimer.Instance.AddBonusTime(
            boardRules.bonusTimeOnLevelClear
        );

        // Determine the next level's parameters based on current state and rules
        var current = new LevelProgressionState
        {
            level = currentLevel,
            boardSize = currentBoardSize,
            enemyCount = currentEnemyCount
        };

        var next =
            LevelProgressionLogic.Next(
                current,
                boardRules
            );

        currentLevel = next.level;
        currentBoardSize = next.boardSize;
        currentEnemyCount = next.enemyCount;

        StartLevel();
    }
}