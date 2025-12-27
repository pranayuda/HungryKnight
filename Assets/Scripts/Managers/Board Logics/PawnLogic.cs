using UnityEngine;
using System.Collections.Generic;

// Handles the logic for pawns on the board, including their positions and captures.
public class PawnLogic
{
    // List to keep track of all pawns currently on the board
    private List<PawnController> pawns = new();

    // Sets the initial list of pawns on the board
    public void SetInitialPawns(List<PawnController> initial)
    {
        pawns = initial;
    }

    // Retrieves the pawn at the specified position, if any
    public PawnController GetPawnAt(Vector2Int pos)
    {
        foreach (var pawn in pawns)
        {
            if (pawn != null && pawn.GridPosition == pos)
                return pawn;
        }
        return null;
    }

    // Checks if the specified position is occupied by a pawn or the knight
    public bool IsOccupied(Vector2Int pos, KnightController knight)
    {
        if (knight != null && knight.GridPosition == pos)
            return true;

        return GetPawnAt(pos) != null;
    }

    // Adds a new pawn to the list of pawns on the board
    public void AddPawn(PawnController pawn)
    {
        if (pawn != null)
            pawns.Add(pawn);
    }

    // Attempts to capture (remove) the pawn at the specified position
    public bool TryCapturePawn(Vector2Int pos)
    {
        PawnController pawn = GetPawnAt(pos);
        if (pawn == null)
            return false;

        pawns.Remove(pawn);
        Object.DestroyImmediate(pawn.gameObject);
        return true;
    }
    
    // Checks if there are any remaining pawns on the board
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