namespace Awktion.Domain.Entities.Games.Handlers;

public delegate void TickOccurredEventHandler(object sender,TickOccurredEventArgs args);

public class TickOccurredEventArgs : EventArgs
{
    public int Mins { get; private set;}
    public int Secs { get; private set;}
    public TickOccurredEventArgs(int mins,int secs)
    {
        Mins = mins;
        Secs = secs;
    }

}
