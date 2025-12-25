using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Progression Settings")]
    [SerializeField] private int startEnemyCount = 3;
    [SerializeField] private int maxEnemyCount = 60;

    int currentLevel = 1;
    int currentEnemyCount;

    [Header("References")]
    [SerializeField] private BoardController boardController;

    void Start()
    {
        currentEnemyCount = startEnemyCount;
        StartLevel();
    }

    void StartLevel()
    {
        Debug.Log($"=== LEVEL {currentLevel} ===");
        Debug.Log($"Enemy Count: {currentEnemyCount}");

        boardController.GenerateLevel(currentEnemyCount);
    }

    public void OnLevelCleared()
    {
        currentLevel++;

        if (currentEnemyCount < maxEnemyCount)
            currentEnemyCount++;

        StartLevel();
    }
}