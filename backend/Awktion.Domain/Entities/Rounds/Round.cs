using System.Diagnostics.Contracts;
using System.Timers;
using Awktion.Domain.Entities.Games.Handlers;
using Awktion.Domain.Entities.Players;
using Awktion.Domain.Entities.Users;
using Timer = System.Timers.Timer;

namespace Awktion.Domain.Entities.Rounds;


/// <summary>
/// Current round specific details and helper methods.
/// </summary>
public class Round
{
    public readonly int No;
    private User? _winner;
    private int _highestBid;
    private Player? _picked;
    public event Action<Player>? OnBiddingStarted;
    public event TickOccurredEventHandler OnTickOccurred;
    public event Action? OnTimerFinished;
    public event Action<Player>? OnPlayerUnsold;
    private Timer? timer;
    public int Mins { get; set; }

    public Round(int no)
    {
        No = no;
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
        OnBiddingStarted?.Invoke(_picked);

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
        RoundEnded();

        if(_picked != null)
        {
            OnPlayerUnsold?.Invoke(_picked);
        }
    }

    private void RoundEnded()
    {
        ClearTimer();
    }
}
