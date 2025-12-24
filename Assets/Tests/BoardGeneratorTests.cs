using NUnit.Framework;
using UnityEngine;

public class BoardGeneratorTests
{
    private BoardRules rules;
    private BoardGenerator generator;

    [SetUp]
    public void Setup()
    {
        rules = ScriptableObject.CreateInstance<BoardRules>();
        rules.minSize = 3;
        rules.maxSize = 8;
        rules.densityThresholdToGrow = 0.7f;

        generator = new BoardGenerator(rules);
    }

    [Test]
    public void Board_Grows_When_Density_Above_Threshold()
    {
        int currentSize = 3;
        int enemyCount = 7; // 7/9 ≈ 0.78

        BoardRuntimeData data =
            generator.GenerateBoardData(currentSize, enemyCount);

        Assert.AreEqual(4, data.width);
    }

    [Test]
    public void Board_Does_Not_Grow_When_Density_Low()
    {
        int currentSize = 4;
        int enemyCount = 5; // 5/16 = 0.31

        BoardRuntimeData data =
            generator.GenerateBoardData(currentSize, enemyCount);

        Assert.AreEqual(4, data.width);
    }
}
