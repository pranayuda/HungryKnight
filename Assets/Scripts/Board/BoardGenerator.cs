using UnityEngine;

public class BoardGenerator
{
    // Board generation rules configuration
    private BoardRules rules;
    public BoardGenerator(BoardRules rules)
    {
        // Store the provided rules for board generation
        this.rules = rules;
    }

    // Generate board runtime data based on current size and enemy count
    public BoardRuntimeData GenerateBoardData(
    int currentBoardSize,
    int enemyCount
    )
    {
        // Ensure current size is within min and max bounds
        int safeCurrentSize = Mathf.Clamp(
            currentBoardSize,
            rules.minSize,
            rules.maxSize
        );

        // Determine next board size based on enemy density
        int nextSize = safeCurrentSize;

        float density =
            (float)enemyCount / (safeCurrentSize * safeCurrentSize);

        // Increase board size if density exceeds threshold
        if (density >= rules.densityThresholdToGrow)
        {
            nextSize = Mathf.Min(
                safeCurrentSize + 1,
                rules.maxSize
            );
        }

        // Create and return the runtime data for the new board
        return new BoardRuntimeData
        {
            width = nextSize,
            height = nextSize,
            tileSize = rules.tileSize,
            lightTileColor = rules.lightTileColor,
            darkTileColor = rules.darkTileColor
        };
    }
}