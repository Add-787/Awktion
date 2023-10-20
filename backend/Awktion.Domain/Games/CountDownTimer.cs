namespace Awktion.Domain.Games;

public class CountDownTimer
{
    private TimeSpan Time { get; set; }
    private Thread Counter { get; set; }
    public CountDownTimer(TimeSpan time)
    {
        Time = time;
    }

    public void RestartTimer()
    {

        if(Counter != null && Counter.IsAlive)
        {
            Counter.Join();
        }

        Counter = new Thread(async () =>
        {
            var current = Time;

            while (current != TimeSpan.Zero)
            {
                await Task.Delay(1000);
                current = current.Subtract(TimeSpan.FromSeconds(1));
                TickOccurredArgs args = new()
                {
                    Minutes = current.Minutes,
                    Seconds = current.Seconds
                };
                OnTickOccurred(args);
            }
            OnTimerFinished(new EventArgs());
        });

        Counter.Start();
    }

    protected virtual void OnTickOccurred(TickOccurredArgs e)
    {
        EventHandler<TickOccurredArgs> handler = TickOccurred;

        if(handler != null)
        {
            handler(this, e);
        }
    }
    public event EventHandler<TickOccurredArgs> TickOccurred;
    public class TickOccurredArgs : EventArgs
    {
        public int Minutes { get; set; }
        public int Seconds { get; set; }
    }

    protected virtual void OnTimerFinished(EventArgs e)
    {
        EventHandler<EventArgs> handler = TimerFinished;

        if(handler != null)
        {
            handler(this, e);
        }
    }
    public event EventHandler<EventArgs> TimerFinished;
    

}


