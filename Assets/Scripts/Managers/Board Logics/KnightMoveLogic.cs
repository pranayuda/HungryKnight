using UnityEngine;

// Handles the logic for knight movements and interactions with pawns on the board.
public class KnightMoveLogic
{
    private PawnLogic pawnLogic;

    // Constructor to initialize with pawn logic to manage pawn interactions
    public KnightMoveLogic(PawnLogic pawnLogic)
    {
        this.pawnLogic = pawnLogic;
    }

    // Resolves the knight's move to the target position, capturing pawns if present
    public void ResolveMove(
        KnightController knight,
        Vector2Int target,
        int boardWidth,
        int boardHeight
    )
    {
        // Check if there was a pawn at the target position before moving
        bool hadPawn = pawnLogic.GetPawnAt(target) != null;

        bool moved = knight.TryMove(target, boardWidth, boardHeight);
        if (!moved)
            return;

        if (!hadPawn)
        {
            // Invalid move if no pawn was captured
            GameManager.Instance.OnInvalidMove();
            return;
        }

        // Capture the pawn at the target position
        pawnLogic.TryCapturePawn(target);
        SFXManager.Instance.PlayCaptureSound();
        ChessTimer.Instance.OnPlayerMove();
    }

    // Checks if there are any possible capture moves for the knight
    public static bool HasAnyCaptureMove(
        Vector2Int knightPos,
        int boardWidth,
        int boardHeight,
        System.Func<Vector2Int, bool> hasPawnAt
    )
    {
        // Iterate through all possible knight moves
        foreach (var move in KnightMoveRules.Moves)
        {
            Vector2Int target = knightPos + move;

            if (target.x < 0 || target.x >= boardWidth ||
                target.y < 0 || target.y >= boardHeight)
                continue;

            if (hasPawnAt(target))
                return true;
        }

        // No capture moves available and no extra pawns left
        if (!ExtraPawnManager.Instance.CanUseExtraPawn())
            GameManager.Instance.OnDeadlockWithoutExtraPawns();

        return false;
    }
}