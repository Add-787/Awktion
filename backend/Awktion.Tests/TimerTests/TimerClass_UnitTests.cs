

using Awktion.Domain.Game;

namespace Awktion.Tests.TimerTests;

public class CountDownTimerClass_UnitTests
{
    [Fact]
    public void IsTimer_Created()
    {
        var timer = new CountDownTimer(new TimeSpan(0, 1, 0));

        Assert.True(timer != null, "The countdown timer was not created.");
    }

    [Fact]
    public void IsTimer_TickOccurred_EventRaised()
    {
        var timer = new CountDownTimer(new TimeSpan(0, 1, 0));
        var resetEvent = new ManualResetEventSlim(false);

        timer.RestartTimer();

        timer.TickOccurred += (t, e) =>
        {
            resetEvent.Set();
        };

        var eventSignaled = resetEvent.Wait(2000);

        // Assert that the event occurred
        Assert.True(eventSignaled, "Event occurred within the timeout period.");
        
    }

    [Fact]
    public async void IsTimer_TickOccurred_CheckNoOfTicksOccurred()
    {
        var timer = new CountDownTimer(new TimeSpan(0, 1, 0));
        var eventCompleted = new TaskCompletionSource<bool>();
        var ticksOccurred = 0;

        timer.TickOccurred += (t,e) => 
        {
            ticksOccurred++;

            if(ticksOccurred == 2)
            {
                eventCompleted.SetResult(true);
            }
        };

        timer.RestartTimer();

        bool eventCompletedSuccessfully = await Task.WhenAny(eventCompleted.Task, Task.Delay(2500)) == eventCompleted.Task;

        Assert.True(eventCompletedSuccessfully, "Ticks did not occur for the necessary amount of times");

    }

    [Theory]
    [InlineData(0,58,2,0,56)]
    [InlineData(1,1,2,0,59)]
    public async void IsTimer_TickOccurred_CheckMinutesAndSeconds(int min,int sec,int maxTicks,int expectedMins,int expectedSeconds)
    {
        var timer = new CountDownTimer(new TimeSpan(0, min, sec));
        var eventCompleted = new TaskCompletionSource<bool>();
        var currentSeconds = -1;
        var currentMins = -1;
        var ticks = 0;

        timer.TickOccurred += (t, e) =>
        {
            ticks++;
            if(ticks == maxTicks)
            {
                currentSeconds = e.Seconds;
                currentMins = e.Minutes;
                eventCompleted.SetResult(true);
            }
        };

        timer.RestartTimer();

        bool eventCompletedSuccessfully = await Task.WhenAny(eventCompleted.Task, Task.Delay(2500)) == eventCompleted.Task;

        Assert.True(eventCompletedSuccessfully, "Ticks did not occur for the necessary amount of times");
        Assert.True(currentMins == expectedMins, $"Wrong value for current value of minutes - Showing {currentMins} minutes");
        Assert.True(currentSeconds == expectedSeconds, $"Wrong value for current value of seconds - Showing {currentSeconds} seconds");
    }

    [Fact]
    public async void IsTimer_ConsecutiveRestarts()
    {
        var span = new TimeSpan(0, 1, 0);
        var timer = new CountDownTimer(span);
        var eventCompleted = new TaskCompletionSource<bool>();
        var currSec = -1;
        var currMin = -1;
        var ticks = 0;

        timer.TickOccurred += (t, e) =>
        {
            ticks++;
            if(ticks == 3)
            {
                currMin = e.Minutes;
                currSec = e.Seconds;
                eventCompleted.SetResult(true);
            }

        };

        timer.RestartTimer();
        await Task.Delay(2000);
        timer.RestartTimer();

        bool eventCompletedSuccessfully = await Task.WhenAny(eventCompleted.Task, Task.Delay(3500)) == eventCompleted.Task;

        Assert.True(eventCompletedSuccessfully, "Ticks did not occur for the necessary amount of times");
        Assert.True(currMin == 0, $"Wrong value for current value of minutes - Showing {currMin} minutes");
        Assert.True(currSec == 59, $"Wrong value for current value of seconds - Showing {currSec} seconds");

    }


}
