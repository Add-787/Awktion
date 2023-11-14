using System.Diagnostics.Contracts;
using System.Timers;
using Awktion.Domain.Entities.Games.Handlers;
using Awktion.Domain.Entities.Players;
using Timer = System.Timers.Timer;

namespace Awktion.Domain.Entities.Rounds;


/// <summary>
/// Current round specific details and helper methods.
/// </summary>
public class Round
{
    public int No { get; set; }
    private User? _winner;
    private int _highestBid;
    private Player? _picked;
    public event Action? OnBiddingStarted;
    public event TickOccurredEventHandler OnTickOccurred;
    public event Action? OnTimerFinished;
    public event Action<Player>? OnPlayerUnSold;
    private Timer? timer;
    public int Mins { get; set; }

    public Round()
    {

    }

    public Round(int no, int mins)
    {
        No = no;
        Mins = mins;
    }

    public bool Bid(User user, int amount)
    {
        if(amount > _highestBid)
        {
            _winner = user;
            RestartTimer(TimeSpan.FromMinutes(Mins));
            return true;
        }

        return false;
    }

    public User? GetCurrentWinner()
    {
        return _winner;
    }

    public int GetHighestBid()
    {
        return _highestBid;
    }

    public Player? GetCurrentPlayer()
    {
        return _picked;
    }

    public int GetCurrentRound()
    {
        return No;
    }

    public void Picked(Player player)
    {
        _picked = player;
        _highestBid = player.BasePrice;

        // Broadcast client to start bidding
        OnBiddingStarted?.Invoke();

        RestartTimer(TimeSpan.FromMinutes(Mins));
    }

    private void RestartTimer(TimeSpan curr)
    {
        ClearTimer();
        timer = new Timer(TimeSpan.FromSeconds(1));
        timer.Elapsed += (object? sender, ElapsedEventArgs e) => {
            curr -= TimeSpan.FromSeconds(1);

            OnTickOccurred?.Invoke(this, new TickOccurredEventArgs(curr.Minutes,curr.Seconds));

            if(curr == TimeSpan.Zero)
            {
                OnTimerFinished?.Invoke();
            }  
        };
        timer.Start();
    }

    private void ClearTimer()
    {
        if(timer != null)
        {
            timer.Stop();
            timer.Dispose();
        }
    }

    public void AllHandsDown()
    {
        ClearTimer();

        if(_picked != null)
        {
            OnPlayerUnSold?.Invoke(_picked);
        }
    }

    public void RoundEnded()
    {
        ClearTimer();
    }
}
