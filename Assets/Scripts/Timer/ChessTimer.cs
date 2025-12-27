using UnityEngine;

// Manages the chess timer for players, including time tracking and increments.
public class ChessTimer : MonoBehaviour
{
    public static ChessTimer Instance { get; private set; }
    ChessTimerLogic logic;

    float baseTime;
    float increment;

    public float TimeRemaining =>
        logic != null ? logic.TimeRemaining : 0f;

    void Awake()
    { 
        Instance = this;
    }

    public void Init(float baseTime, float increment)
    {
        this.baseTime = baseTime;
        this.increment = increment;

        if (logic == null)
        {
            logic = new ChessTimerLogic(baseTime, increment);
        }
    }

    public void ResetTimer()
    {
        logic = new ChessTimerLogic(baseTime, increment);
    }

    // Updates the timer each frame during gameplay
    void Update()
    {
        if (logic == null || GameManager.Instance.State != GameState.Playing)
            return;

        logic.Tick(Time.deltaTime);

        if (logic.IsTimeUp())
        {
            GameManager.Instance.OnTimeUp();    
        }
    }

    public void OnPlayerMove()
    {
        logic.OnMoveCompleted();
    }

    public void AddBonusTime(float bonus)
    {
        logic.AddTime(bonus);
    }
}