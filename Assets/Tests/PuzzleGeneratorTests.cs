using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PuzzleGeneratorTests
{
    [Test]
    public void Generated_Puzzle_Is_Solvable_By_Construction()
    {
        int boardSize = 6;
        int enemyCount = 6;

        PuzzleGenerator generator =
            new PuzzleGenerator(boardSize, boardSize, enemyCount);

        bool success = generator.TryGeneratePuzzle(
            out Vector2Int knightStart,
            out List<Vector2Int> pawnPositions,
            out List<Vector2Int> solutionPath
        );

        Assert.IsTrue(success, "Puzzle generation failed");

        // Path length = knight + pawns
        Assert.AreEqual(
            enemyCount + 1,
            solutionPath.Count,
            "Invalid solution path length"
        );

        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();

        for (int i = 0; i < solutionPath.Count - 1; i++)
        {
            Vector2Int from = solutionPath[i];
            Vector2Int to = solutionPath[i + 1];

            Assert.IsTrue(
                KnightMoveRules.IsValidMove(from, to),
                $"Invalid knight move from {from} to {to}"
            );

            Assert.IsFalse(
                visited.Contains(from),
                $"Duplicate tile detected at {from}"
            );

            visited.Add(from);
        }

        // Pawn count check
        Assert.AreEqual(
            enemyCount,
            pawnPositions.Count,
            "Pawn count mismatch"
        );
    }
}