using UnityEngine;

[CreateAssetMenu(fileName = "BoardRules", menuName = "Scriptable Objects/BoardRules")]

public class BoardRules : ScriptableObject
{
    public int minSize = 3;
    public int maxSize = 8;

    [Header("Tile Settings")]
    public float tileSize = 1f;
    public Color lightTileColor;
    public Color darkTileColor;

    [Header("Difficulty Thresholds")]
    public float densityThresholdToGrow = 0.8f;
}