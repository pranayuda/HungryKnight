using UnityEngine;

public class ChessTimer : MonoBehaviour
{
    public static ChessTimer Instance { get; private set; }
    ChessTimerLogic logic;

    public float TimeRemaining =>
        logic != null ? logic.TimeRemaining : 0f;

    void Awake()
    { 
        Instance = this;
    }

    public void Init(float baseTime, float increment)
    {
        if (logic != null)
            return;

        logic = new ChessTimerLogic(baseTime, increment);
    }

    void Update()
    {
        if (logic == null || GameManager.Instance.State != GameState.Playing)
            return;

        logic.Tick(Time.deltaTime);

        if (logic.IsTimeUp())
        {
            if (GameManager.Instance.State != GameState.Playing)
                return;
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