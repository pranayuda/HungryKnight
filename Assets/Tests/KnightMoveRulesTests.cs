using NUnit.Framework;
using UnityEngine;

public class KnightMoveRulesTests
{
    [Test]
    public void Knight_Move_1_2_Is_Valid()
    {
        Vector2Int from = new Vector2Int(3, 3);
        Vector2Int to = new Vector2Int(4, 5);

        bool valid = KnightMoveRules.IsValidMove(from, to);

        Assert.IsTrue(valid);
    }

    [Test]
    public void Knight_Move_2_2_Is_Invalid()
    {
        Vector2Int from = new Vector2Int(3, 3);
        Vector2Int to = new Vector2Int(5, 5);

        bool valid = KnightMoveRules.IsValidMove(from, to);

        Assert.IsFalse(valid);
    }

    [Test]
    public void Knight_Move_Straight_Is_Invalid()
    {
        Vector2Int from = new Vector2Int(3, 3);
        Vector2Int to = new Vector2Int(3, 5);

        bool valid = KnightMoveRules.IsValidMove(from, to);

        Assert.IsFalse(valid);
    }
}