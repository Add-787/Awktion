
using System.Runtime.InteropServices;
using Awktion.Domain.Models;
using static Awktion.Domain.Games.CountDownTimer;

namespace Awktion.Domain.Games;

public class Game
{
    private readonly GameSettings Settings;
    private readonly List<User> Users;

    // private CountDownTimer Timer;

    // Round specific details.
    private int CurrentRound = 0;
    private User? CurrentWinner = null;
    private Player? Picked = null;
    private int HighestBid = 0;
    private bool CanBid = false;
    private int NoOfUsers
    {
        get
        {
            return Users.Count;
        }
    }


    public Game(GameSettings settings, List<User> users)
    {
        Settings = settings;
        Users = users;
    }

    public void InitBalances()
    {
        Users.ForEach(u => u.Balances = Settings.TotalBalance);
    }

    public void InitSquads()
    {
        foreach (User user in Users)
        {
            user.Squad = new();
        }
    }

    public void InitStatuses()
    {
        Users.ForEach(u => u.Status = BidStatus.Open);
    }

    public void Start()
    {
        InitBalances();
        InitSquads();
        CreateTimer();

        NewRound();
    }

    private void CreateTimer()
    {
        // Timer = new CountDownTimer(Settings.TimeSpan);

        // Timer.TickOccurred += (object sender, TickOccurredArgs args) =>
        // {
        //     Console.WriteLine($"Minutes: {args.Minutes}, Seconds: {args.Seconds}");
        // };

        // Timer.TimerFinished += (object sender, EventArgs args) =>
        // {
        //     if(CurrentWinner != null)
        //     {
        //         BidAccepted();
        //     }

        //     if(Picked != null)
        //     {
        //         Settings.AvailablePlayers.Remove(Picked);
        //     }
            
        //     EndRound();
        // };

    }

    private void NewRound()
    {
        CurrentRound++;
        // Broadcast start of new Round and provide the client user info on whose picking.
        OnRoundStarted(new RoundStartedArgs {
            Picking = Users.ElementAt(CurrentRound - 1 % NoOfUsers),
            Round = CurrentRound
        });

    }

    public bool PlayerPicked(Player player)
    {
        if (Settings.AvailablePlayers.Contains(player))
        {
            StartBidding(player);
            return true;
        }

        return false;
    }

    private void StartBidding(Player player)
    {
        Picked = player;
        HighestBid = player.BasePrice;
        InitStatuses();
        // Timer.Restart();
        CanBid = true;

        // Broadcast Player has been picked and bidding can start;
        OnBiddingStarted(new BiddingStartedArgs{
            Picked = player
        });

    }

    private void EndRound()
    {
        CurrentWinner = null;
        Picked = null;

        if (CurrentRound < Settings.AvailablePlayers.Count)
        {
            NewRound();
        }

        EndGame();
    }

    private void EndGame()
    {
        // Timer.Stop();

        // Broadcast that current game has ended.
        OnGameEnded(EventArgs.Empty);

    }

    public bool Bid(User user, int amount)
    {
        if (!CanBid)
        {
            return false;
        }

        if (Picked == null)
        {
            return false;
        }

        if (amount + HighestBid > user.Balances)
        {
            return false;
        }

        HighestBid += amount;
        CurrentWinner = user;

        //Broadcast new bid placed.
        OnNewBidPlaced(new NewBidPlacedArgs
        {
            User = CurrentWinner,
            Player = Picked,
            Amount = HighestBid
        });

        // Timer.Restart();

        return true;
    }

    public void PutDown(User user)
    {
        user.Status = BidStatus.Closed;

        // Broadcast that given user is out for the current player
        OnUserOut(new UserOutArgs
        {
            User = user
        });

        // Check if all are out for bidding on current player.
        var count = Users.Where(u => u.Status == BidStatus.Open).ToList().Count;

        if (CurrentWinner != null && CurrentWinner.Status == BidStatus.Open && count == 1)
        {
            BidAccepted();
        }

        // Every user is out for current round
        if(count == 0)
        {
            EndRound();
        }

    }

    private void BidAccepted()
    {
        if (Picked == null)
        {
            return;
        }

        CurrentWinner!.Squad.Add((Picked, HighestBid));

        CurrentWinner!.Balances -= HighestBid;

        // Broadcast that player is sold.
        OnPlayerSold(new PlayerSoldArgs
        {
            Player = Picked,
            User = CurrentWinner!,
            Price = HighestBid
        });

        CanBid = false;
        // End current round.
        EndRound();

    }

    protected void OnRoundStarted(RoundStartedArgs args)
    {
        RoundStarted?.Invoke(this, args);
    }
    public event EventHandler<EventArgs> RoundStarted;

    protected virtual void OnBiddingStarted(BiddingStartedArgs args)
    {
        BiddingStarted?.Invoke(this, args);
    }
    public event EventHandler<EventArgs> BiddingStarted;

    protected void OnPlayerSold(PlayerSoldArgs args)
    {
        PlayerSold?.Invoke(this, args);
    }
    public event EventHandler<EventArgs> PlayerSold;

    protected void OnNewBidPlaced(NewBidPlacedArgs args)
    {
        NewBidPlaced?.Invoke(this, args);
    }
    public event EventHandler<NewBidPlacedArgs> NewBidPlaced;

    protected void OnGameEnded(EventArgs args)
    {
        GameEnded?.Invoke(this, args);
    }
    public event EventHandler<EventArgs> GameEnded;

    protected void OnUserOut(UserOutArgs args)
    {
        UserOut?.Invoke(this,args);
    }
    public event EventHandler<UserOutArgs> UserOut;

}

public class BiddingStartedArgs : EventArgs
{
    public required Player Picked;
}

public class UserOutArgs : EventArgs
{
    public required User User;
}

public class RoundStartedArgs : EventArgs
{
    public required User Picking { get; set; }
    public int Round;
}

public class PlayerSoldArgs : EventArgs
{
    public required User User { get; set; }
    public required Player Player { get; set; }
    public int Price { get; set; }
}
public class NewBidPlacedArgs : EventArgs 
{
    public required User User { get; set; }
    public required Player Player { get; set; }
    public int Amount { get; set; }
}

public enum BidStatus {
    Open, Closed
}
