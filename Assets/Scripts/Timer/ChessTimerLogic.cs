using UnityEngine;

// Handles the core logic of the chess timer, including time decrement and increments.
// Important to separate from MonoBehaviour for easier testing and logic management.
public class ChessTimerLogic
{
    public float TimeRemaining { get; private set; }
    public float Increment { get; private set; }

    public ChessTimerLogic(float baseTime, float increment)
    {
        TimeRemaining = baseTime;
        Increment = increment;
    }

    public void Tick(float deltaTime)
    {
        TimeRemaining -= deltaTime;
        if (TimeRemaining < 0)
            TimeRemaining = 0;
    }

    public void OnMoveCompleted()
    {
        TimeRemaining += Increment;
    }

    public bool IsTimeUp()
    {
        return TimeRemaining <= 0;
    }

    public void AddTime(float amount)
    {
        TimeRemaining += amount;
    }
}