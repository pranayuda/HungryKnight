using UnityEngine;

// Manages level progression, including board size and enemy count
public class LevelManager : MonoBehaviour
{
    int currentLevel;
    int currentEnemyCount;
    int currentBoardSize;

    [Header("References")]
    [SerializeField] private BoardRules boardRules;

    void Start()
    {
        ChessTimer.Instance.Init(
            boardRules.baseTimeSeconds,
            boardRules.incrementPerMove
        );

        currentLevel = 1;
        currentEnemyCount = boardRules.enemiesToSpawn;
        currentBoardSize = boardRules.minSize;

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

    // Called when the current level is cleared to progress to the next level
    public void OnLevelCleared()
    {
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