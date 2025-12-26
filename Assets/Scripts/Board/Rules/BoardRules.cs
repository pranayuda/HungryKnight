using UnityEngine;

[CreateAssetMenu(fileName = "BoardRules", menuName = "Scriptable Objects/BoardRules")]

public class BoardRules : ScriptableObject
{
    public int minSize = 3;
    public int maxSize = 8;
    public int maxEnemies = 60;

    [Header("Tile Settings")]
    public float tileSize = 1f;
    public Color lightTileColor;
    public Color darkTileColor;

    [Header("Difficulty Thresholds")]
    public int enemiesToSpawn = 2;
    public int sizeToIncreaseDifficulty = 1;
    public int enemiesToIncreaseDifficulty = 1;
    public float densityThresholdToGrow = 0.8f;

    [Header("Timer Settings")]
    public float baseTimeSeconds = 120f;   
    public float incrementPerMove = 1f;    
    public float bonusTimeOnLevelClear = 5f;

    [Header("Extra Pawn Settings")]
    public int extraPawnCount = 1;
}