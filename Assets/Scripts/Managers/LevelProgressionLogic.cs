public struct LevelProgressionState
{
    public int level;
    public int boardSize;
    public int enemyCount;
}

// Logic to determine the next level's parameters based on current state and rules
// public static class is used to allow easy access without instantiation and unit testing
public static class LevelProgressionLogic
{
    public static LevelProgressionState Next(
        LevelProgressionState current,
        BoardRules rules
    )
    {
        LevelProgressionState next = current;

        next.level++;

        if (next.enemyCount < rules.maxEnemies)
            next.enemyCount++;

        float density =
            (float)next.enemyCount /
            (next.boardSize * next.boardSize);

        if (
            density >= rules.densityThresholdToGrow &&
            next.boardSize < rules.maxSize
        )
        {
            next.boardSize++;
        }

        return next;
    }
}