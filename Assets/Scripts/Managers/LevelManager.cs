using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Progression Settings")]

    int currentLevel = 1;
    int currentEnemyCount;
    int currentBoardSize;

    [Header("References")]
    [SerializeField] private BoardController boardController;
    [SerializeField] private BoardRules boardRules;

    void Start()
    {
        currentEnemyCount = boardRules.enemiesToSpawn;
        currentBoardSize = boardRules.minSize;
        StartLevel();
    }

    void StartLevel()
    {
        Debug.Log($"=== LEVEL {currentLevel} ===");
        Debug.Log($"Board Size: {currentBoardSize}");
        Debug.Log($"Enemy Count: {currentEnemyCount}");

        boardController.GenerateLevel(
            currentBoardSize,
            currentEnemyCount
        );
    }

    public void OnLevelCleared()
    {
        currentLevel++;

        if (currentEnemyCount < boardRules.maxEnemies)
            currentEnemyCount++;

        float density =
            (float)currentEnemyCount /
            (currentBoardSize * currentBoardSize);

        if (
            density >= boardRules.densityThresholdToGrow &&
            currentBoardSize < boardRules.maxSize
        )
        {
            currentBoardSize++;
            Debug.Log($"Board size increased to {currentBoardSize}x{currentBoardSize}");
        }

        StartLevel();
    }
}