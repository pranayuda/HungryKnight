using UnityEngine;

public class BoardGenerator
{
    private BoardRules rules;

    public BoardGenerator(BoardRules rules)
    {
        this.rules = rules;
    }

    public BoardRuntimeData GenerateBoardData(
    int currentBoardSize,
    int enemyCount
    )
    {
        int safeCurrentSize = Mathf.Clamp(
            currentBoardSize,
            rules.minSize,
            rules.maxSize
        );

        int nextSize = safeCurrentSize;

        float density =
            (float)enemyCount / (safeCurrentSize * safeCurrentSize);

        if (density >= rules.densityThresholdToGrow)
        {
            nextSize = Mathf.Min(
                safeCurrentSize + 1,
                rules.maxSize
            );
        }

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