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
}