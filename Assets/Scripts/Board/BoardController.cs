using UnityEngine;

public class BoardController : MonoBehaviour
{
    [Header("Rules")]
    [SerializeField] private BoardRules boardRules;

    [Header("References")]
    [SerializeField] private BoardView boardView;

    [Header("Runtime")]
    [SerializeField] private int currentBoardSize = 3;
    [SerializeField] private int currentEnemyCount = 1;

    private BoardGenerator boardGenerator;

    void Start()
    {
        boardGenerator = new BoardGenerator(boardRules);
        GenerateLevel();
    }

    void GenerateLevel()
    {
        BoardRuntimeData boardData =
            boardGenerator.GenerateBoardData(
                currentBoardSize,
                currentEnemyCount
            );

        boardView.Generate(boardData);

        currentBoardSize = boardData.width;
    }
}