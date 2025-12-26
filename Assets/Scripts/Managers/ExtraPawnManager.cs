using UnityEngine;

public class ExtraPawnManager : MonoBehaviour
{
    public static ExtraPawnManager Instance { get; private set; }

    [SerializeField] private GameObject pawnPrefab;

    int extraPawnCount = 1;
    bool isPlacingPawn = false;

    void Awake()
    {
        Instance = this;
    }

    public int ExtraPawnCount => extraPawnCount;
    public bool IsPlacingPawn => isPlacingPawn;

    // Dipanggil saat clear 5 board (nanti)
    public void AddExtraPawn(int amount = 1)
    {
        extraPawnCount += amount;
        Debug.Log($"Extra Pawn +{amount}, total = {extraPawnCount}");
    }

    public void StartPlacingPawn()
    {
        if (extraPawnCount <= 0)
            return;

        isPlacingPawn = true;
        Debug.Log("PLACE EXTRA PAWN MODE");
    }

    public void CancelPlacingPawn()
    {
        isPlacingPawn = false;
    }

    public PawnController PlacePawn(
        Transform parent,
        Vector2Int gridPos,
        float tileSize,
        Vector2 boardOffset
    )
    {
        if (!isPlacingPawn || extraPawnCount <= 0)
            return null;

        GameObject go = Instantiate(pawnPrefab, parent);
        PawnController pawn = go.GetComponent<PawnController>();
        pawn.Init(gridPos, tileSize, boardOffset);

        extraPawnCount--;
        isPlacingPawn = false;

        Debug.Log($"Pawn placed. Remaining: {extraPawnCount}");

        return pawn;
    }
}