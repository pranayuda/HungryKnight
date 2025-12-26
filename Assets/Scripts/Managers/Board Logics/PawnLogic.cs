using UnityEngine;
using System.Collections.Generic;

public class PawnLogic
{
    private List<PawnController> pawns = new();

    public void SetInitialPawns(List<PawnController> initial)
    {
        pawns = initial;
    }

    public PawnController GetPawnAt(Vector2Int pos)
    {
        foreach (var pawn in pawns)
        {
            if (pawn != null && pawn.GridPosition == pos)
                return pawn;
        }
        return null;
    }

    public bool IsOccupied(Vector2Int pos, KnightController knight)
    {
        if (knight != null && knight.GridPosition == pos)
            return true;

        return GetPawnAt(pos) != null;
    }

    public void AddPawn(PawnController pawn)
    {
        if (pawn != null)
            pawns.Add(pawn);
    }

    public bool TryCapturePawn(Vector2Int pos)
    {
        PawnController pawn = GetPawnAt(pos);
        if (pawn == null)
            return false;

        pawns.Remove(pawn);
        Object.DestroyImmediate(pawn.gameObject);
        return true;
    }

    public bool HasRemainingPawns()
    {
        return pawns.Count > 0;
    }

    public void Clear()
    {
        foreach (var pawn in pawns)
        {
            if (pawn != null)
                Object.DestroyImmediate(pawn.gameObject);
        }
        pawns.Clear();
    }
}