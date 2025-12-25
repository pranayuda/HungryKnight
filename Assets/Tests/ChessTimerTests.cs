using NUnit.Framework;

public class ChessTimerLogicTests
{
    [Test]
    public void Timer_Decreases_Over_Time()
    {
        var timer = new ChessTimerLogic(10f, 1f);

        timer.Tick(3f);

        Assert.AreEqual(7f, timer.TimeRemaining, 0.01f);
    }

    [Test]
    public void Timer_Gets_Increment_On_Move()
    {
        var timer = new ChessTimerLogic(10f, 1f);

        timer.Tick(2f);           // 8
        timer.OnMoveCompleted();  // +1 → 9

        Assert.AreEqual(9f, timer.TimeRemaining, 0.01f);
    }

    [Test]
    public void Timer_Stops_At_Zero()
    {
        var timer = new ChessTimerLogic(2f, 1f);

        timer.Tick(5f);

        Assert.AreEqual(0f, timer.TimeRemaining);
        Assert.IsTrue(timer.IsTimeUp());
    }
}