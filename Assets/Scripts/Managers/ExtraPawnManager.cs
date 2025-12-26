using UnityEngine;

public class ExtraPawnManager : MonoBehaviour
{
    public static ExtraPawnManager Instance { get; private set; }

    [SerializeField] GameObject pawnPrefab;

    public int ExtraPawnCount { get; private set; }
    public bool IsPlacingPawn { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public void ResetExtraPawns()
    {
        ExtraPawnCount = 1;
        IsPlacingPawn = false;
    }

    public void AddExtraPawn(int amount = 1)
    {
        ExtraPawnCount += amount;
    }

    public bool CanUseExtraPawn()
    {
        return ExtraPawnCount > 0;
    }

    public void StartPlacingPawn()
    {
        if (!CanUseExtraPawn())
            return;

        IsPlacingPawn = true;
    }

    public PawnController PlacePawn(
        Transform parent,
        Vector2Int pos,
        float tileSize,
        Vector2 boardOffset
    )
    {
        if (!IsPlacingPawn)
            return null;

        GameObject go = Instantiate(pawnPrefab, parent);
        PawnController pawn = go.GetComponent<PawnController>();
        pawn.Init(pos, tileSize, boardOffset);

        ExtraPawnCount--;
        IsPlacingPawn = false;
        return pawn;
    }

    public void CancelPlacingPawn()
    {
        IsPlacingPawn = false;
    }
}