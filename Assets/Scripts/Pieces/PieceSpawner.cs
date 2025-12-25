using UnityEngine;
using System.Collections.Generic;

// Class responsible for spawning knight on the board
public class PieceSpawner : MonoBehaviour
{
    [SerializeField] private GameObject knightPrefab;
    [SerializeField] private GameObject pawnPrefab;

    // Spawns a knight at a random position on the board
    public KnightController SpawnKnightAt(
        Transform parent,
        Vector2Int pos,
        float tileSize,
        Vector2 boardOffset
    )
    {
        GameObject go = Instantiate(knightPrefab, parent);
        KnightController knight = go.GetComponent<KnightController>();
        knight.Init(pos, tileSize, boardOffset);
        return knight;
    }

    public List<PawnController> SpawnPawnsAt(
        Transform parent,
        List<Vector2Int> positions,
        float tileSize,
        Vector2 boardOffset
    )
    {
        List<PawnController> pawns = new List<PawnController>();

        foreach (var pos in positions)
        {
            GameObject go = Instantiate(pawnPrefab, parent);
            PawnController pawn = go.GetComponent<PawnController>();
            pawn.Init(pos, tileSize, boardOffset);
            pawns.Add(pawn);
        }

        return pawns;
    }

    Vector2Int GetRandomPosition(
        int width,
        int height,
        HashSet<Vector2Int> blocked
    )
    {
        while (true)
        {
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);

            Vector2Int pos = new Vector2Int(x, y);

            if (blocked == null || !blocked.Contains(pos))
                return pos;
        }
    }
}