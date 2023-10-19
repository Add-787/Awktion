
using System.Runtime.InteropServices;
using Awktion.Domain.Models;

namespace Awktion.Domain.Game;

public class Game
{
    public Room Room { get; set; }
    private GameSettings Settings { get; set;  }
    private readonly Dictionary<User, int> Balances = new();
    private readonly Dictionary<User,IList<(Player,int)>> Squads = new();
    private readonly Dictionary<User, BidStatus> Status = new();
    private CountDownTimer Timer;

    // Round specific details.
    private int CurrentRound = 0;
    private User? CurrentWinner = null;
    private User? Picking = null;
    private Player? Picked = null;
    private int HighestBid = 0;
    private bool CanBid = false;
    private int NoOfUsers { get; set; }


    public Game(Room room, GameSettings settings)
    {
        Room = room;
        Settings = settings;
        NoOfUsers = Room.GetUsers().Count;
    }

    public void InitBalances()
    {
        foreach(User user in Room.GetUsers())
        {
            Balances.Add(user, Settings.TotalBalance);
        }
    }

    public void InitSquads()
    {
        foreach(User user in Room.GetUsers())
        {
            Squads.Add(user,new List<(Player,int)>());
        }
    }

    public void InitStatuses()
    {
        foreach(User user in Room.GetUsers())
        {
            Status.Add(user, BidStatus.Open);
        }
    }

    public void StartGame()
    {
        Room.CloseRoom();
        InitBalances();
        InitSquads();
        CreateTimer();

        NewRound();
    }

    private void CreateTimer()
    {
        Timer = new CountDownTimer(Settings.TimeSpan);
    }

    private void NewRound()
    {
        Picking = Room.GetUsers().ElementAt(CurrentRound % NoOfUsers);

        // Broadcast start of new Round
        OnRoundStarted(new EventArgs());

    }

    public bool PlayerPicked(Player player)
    {
        if(Settings.AvailablePlayers.Contains(player))
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
        Timer.RestartTimer();
        CanBid = true;

        // Broadcast Player has been picked annd bidding can start;
        OnBiddingStarted(new EventArgs());
        
    }

    private void EndRound()
    {
        CurrentWinner = null;
        Picked = null;
        Picking = null;

        CurrentRound++;

        // Broadcast end of current round
    }

    public bool Bid(User user, int amount)
    {
        if(!CanBid)
        {
            return false;
        }

        if(amount + HighestBid > Balances[user])
        {
            return false;
        }

        HighestBid += amount;
        CurrentWinner = user;

        //Broadcast new bid placed.



        return true;
    } 

    public void PutDown(User user)
    {
        Status[user] = BidStatus.Closed;

        // Broadcast that given user is out for the current player
        // OnPlayerOut(..)

        // Check if all are out for bidding on current player.
        var count = Room.GetUsers().Where(u => Status[u] == BidStatus.Open).ToList().Count;


        if(CurrentWinner != null && Status[CurrentWinner!] == BidStatus.Open && count == 1)
        {
            BidAccepted();
        }

        EndRound();
        
    }

    private void BidAccepted()
    {

    }

    protected virtual void OnRoundStarted(EventArgs e)
    {
        EventHandler<EventArgs> handler = RoundStarted;
        if(handler != null)
        {
            handler(this, e);
        }
    }

    public event EventHandler<EventArgs> RoundStarted;

    protected virtual void OnBiddingStarted(EventArgs e)
    {
        EventHandler<EventArgs> handler = BiddingStarted;
        if(handler != null)
        {
            handler(this, e);
        }
    }

    public event EventHandler<EventArgs> BiddingStarted;

}

public enum BidStatus {
    Open, Closed
}
