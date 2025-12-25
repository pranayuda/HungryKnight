using UnityEngine;

// Manages level progression, including board size and enemy count
public class LevelManager : MonoBehaviour
{
    int currentLevel;
    int currentEnemyCount;
    int currentBoardSize;

    [SerializeField] private BoardController boardController;
    [SerializeField] private BoardRules boardRules;

    void Start()
    {
        currentLevel = 1;
        currentEnemyCount = boardRules.enemiesToSpawn;
        currentBoardSize = boardRules.minSize;

        StartLevel();
    }

    void StartLevel()
    {
        boardController.GenerateLevel(
            currentBoardSize,
            currentEnemyCount
        );
    }

    // Called when the current level is cleared to progress to the next level
    public void OnLevelCleared()
    {
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