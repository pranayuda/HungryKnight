using NUnit.Framework;
using UnityEngine;

public class LevelProgressionTests
{
    BoardRules rules;

    [SetUp]
    public void Setup()
    {
        rules = ScriptableObject.CreateInstance<BoardRules>();
        rules.minSize = 3;
        rules.maxSize = 8;
        rules.maxEnemies = 60;
        rules.densityThresholdToGrow = 0.8f;
    }

    [Test]
    public void Enemy_Count_Increases_Each_Level()
    {
        var state = new LevelProgressionState
        {
            level = 1,
            boardSize = 3,
            enemyCount = 2
        };

        var next =
            LevelProgressionLogic.Next(state, rules);

        Assert.AreEqual(3, next.enemyCount);
    }

    [Test]
    public void Board_Size_Increases_When_Density_Threshold_Reached()
    {
        var state = new LevelProgressionState
        {
            level = 5,
            boardSize = 3,
            enemyCount = 8 // 8 / 9 ≈ 0.89
        };

        var next =
            LevelProgressionLogic.Next(state, rules);

        Assert.AreEqual(4, next.boardSize);
    }

    [Test]
    public void Board_Size_Does_Not_Exceed_Max()
    {
        var state = new LevelProgressionState
        {
            level = 20,
            boardSize = 8,
            enemyCount = 60
        };

        var next =
            LevelProgressionLogic.Next(state, rules);

        Assert.AreEqual(8, next.boardSize);
    }
}