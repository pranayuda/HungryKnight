using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class KnightMoveLogicTests
{
    [Test]
    public void HasAnyCaptureMove_ReturnsTrue_WhenPawnReachable()
    {
        Vector2Int knightPos = new Vector2Int(4, 4);

        HashSet<Vector2Int> pawnPositions = new()
        {
            new Vector2Int(6, 5) // knight move (2,1)
        };

        bool result = KnightMoveLogic.HasAnyCaptureMove(
            knightPos,
            8,
            8,
            pos => pawnPositions.Contains(pos)
        );

        Assert.IsTrue(result);
    }

    [Test]
    public void HasAnyCaptureMove_ReturnsFalse_WhenNoPawnReachable()
    {
        Vector2Int knightPos = new Vector2Int(0, 0);

        bool result = KnightMoveLogic.HasAnyCaptureMove(
            knightPos,
            8,
            8,
            pos => false
        );

        Assert.IsFalse(result);
    }
}