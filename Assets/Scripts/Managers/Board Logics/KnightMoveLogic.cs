using UnityEngine;

public class KnightMoveLogic
{
    private PawnLogic pawnLogic;

    public KnightMoveLogic(PawnLogic pawnLogic)
    {
        this.pawnLogic = pawnLogic;
    }

    public void ResolveMove(
        KnightController knight,
        Vector2Int target,
        int boardWidth,
        int boardHeight
    )
    {
        bool hadPawn = pawnLogic.GetPawnAt(target) != null;

        bool moved = knight.TryMove(target, boardWidth, boardHeight);
        if (!moved)
            return;

        if (!hadPawn)
        {
            GameManager.Instance.OnInvalidMove();
            return;
        }

        pawnLogic.TryCapturePawn(target);
        ChessTimer.Instance.OnPlayerMove();
    }

    public static bool HasAnyCaptureMove(
        Vector2Int knightPos,
        int boardWidth,
        int boardHeight,
        System.Func<Vector2Int, bool> hasPawnAt
    )
    {
        foreach (var move in KnightMoveRules.Moves)
        {
            Vector2Int target = knightPos + move;

            if (target.x < 0 || target.x >= boardWidth ||
                target.y < 0 || target.y >= boardHeight)
                continue;

            if (hasPawnAt(target))
                return true;
        }

        return false;
    }
}