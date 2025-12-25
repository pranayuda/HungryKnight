using UnityEngine;
using System.Collections.Generic;

// Class responsible for spawning knight on the board
public class PieceSpawner : MonoBehaviour
{
    [SerializeField] private GameObject knightPrefab;
    [SerializeField] private GameObject pawnPrefab;

    // Spawns a knight at a random position on the board
    public KnightController SpawnKnight(
        Transform parent,
        int boardWidth,
        int boardHeight,
        float tileSize,
        Vector2 boardOffset
    )
    {
        int x = Random.Range(0, boardWidth);
        int y = Random.Range(0, boardHeight);

        GameObject go = Instantiate(knightPrefab, parent);
        KnightController knight = go.GetComponent<KnightController>();

        // Initialize knight's position and board parameters
        knight.Init(
            new Vector2Int(x, y),
            tileSize,
            boardOffset
        );

        return knight;
    }

    public List<PawnController> SpawnPawns(
        Transform parent,
        int count,
        int boardWidth,
        int boardHeight,
        float tileSize,
        Vector2 boardOffset,
        Vector2Int knightPos
    )
    {
        List<PawnController> pawns = new List<PawnController>();
        HashSet<Vector2Int> occupied = new HashSet<Vector2Int>
        {
            knightPos
        };

        while (pawns.Count < count)
        {
            Vector2Int pos = GetRandomPosition(
                boardWidth,
                boardHeight,
                occupied
            );

            occupied.Add(pos);

            GameObject go =
                Instantiate(pawnPrefab, parent);

            PawnController pawn =
                go.GetComponent<PawnController>();

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