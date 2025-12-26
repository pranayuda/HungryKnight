using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PawnLogicTests
{
    PawnLogic pawnLogic;
    GameObject pawnGO;
    PawnController pawn;

    [SetUp]
    public void Setup()
    {
        pawnLogic = new PawnLogic();

        pawnGO = new GameObject("Pawn");
        pawn = pawnGO.AddComponent<PawnController>();
        pawn.Init(new Vector2Int(2, 3), 1f, Vector2.zero);

        pawnLogic.SetInitialPawns(new List<PawnController> { pawn });
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(pawnGO);
    }

    [Test]
    public void GetPawnAt_ReturnsPawn_WhenExists()
    {
        PawnController result = pawnLogic.GetPawnAt(new Vector2Int(2, 3));
        Assert.NotNull(result);
    }

    [Test]
    public void GetPawnAt_ReturnsNull_WhenNotExists()
    {
        PawnController result = pawnLogic.GetPawnAt(new Vector2Int(0, 0));
        Assert.IsNull(result);
    }

    [Test]
    public void TryCapturePawn_RemovesPawn()
    {
        bool captured = pawnLogic.TryCapturePawn(new Vector2Int(2, 3));
        Assert.IsTrue(captured);
        Assert.IsFalse(pawnLogic.HasRemainingPawns());
    }

    [Test]
    public void IsOccupied_ReturnsTrue_WhenPawnPresent()
    {
        bool occupied = pawnLogic.IsOccupied(new Vector2Int(2, 3), null);
        Assert.IsTrue(occupied);
    }

    [Test]
    public void Clear_RemovesAllPawns()
    {
        pawnLogic.Clear();
        Assert.IsFalse(pawnLogic.HasRemainingPawns());
    }
}