using UnityEngine;

// Struct holding runtime data for the board configuration
// such as dimensions, tile size, and colors

// Uses struct for simple data container without behavior
public struct BoardRuntimeData
{
    public int width;
    public int height;
    public float tileSize;

    public Color lightTileColor;
    public Color darkTileColor;
}