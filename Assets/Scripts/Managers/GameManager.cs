using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    bool isGameOver = false;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public bool IsGameOver => isGameOver;

    public void OnTimeUp()
    {
        if (isGameOver)
            return;

        isGameOver = true;
        Debug.Log("GAME OVER: TIME UP");

        // nanti:
        // - disable input
        // - show game over UI
    }
}