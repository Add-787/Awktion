using System.Linq;
using Awktion.Domain.Entities.Games.Handlers;
using Awktion.Domain.Entities.Players;
using Awktion.Domain.Entities.Rounds;
using Awktion.Domain.Entities.Squads;
using Awktion.Domain.Entities.Users;

namespace Awktion.Domain.Entities.Games;

public class Game
{
    private readonly Settings _settings;
    private readonly IList<User> _users;
    private Round _round;
    private readonly RoundBuilder _roundBuilder;
    private readonly SquadManager _squadManager;


    public event TickOccurredEventHandler OnTickOccurred;
    public event Action<User> OnUserPicking;
    public event Action? OnBiddingStarted;

    private Dictionary<int,PlayerStatus> _playerManager;
    private Dictionary<string,UserStatus> _userManager;

    private bool AllUsersOut { 
        get 
        {
            return _userManager.Values.All((status) => status == UserStatus.Out);
        } 
    }

    public Game(Settings settings,List<User> users)
    {
        _settings = settings;
        _users = users;
        _roundBuilder = new();
        _squadManager = new SquadManager(_settings.Budget, users);
        _playerManager = new();
        _userManager = new();
    }

    private void InitPlayerStatuses()
    {
        _settings.AvailablePlayerIds.ForEach((id) => {
            _playerManager.Add(id, PlayerStatus.Waiting);
        });
    }

    private void InitUserStatuses()
    {
        foreach(User user in _users)
        {
            _userManager[user.Email] = UserStatus.Bidding;
        }
    }

    public void Start()
    {
        InitPlayerStatuses();

        //Starting new round
        NewRound();
    }

    private bool OtherHandsDown(User user)
    {
        foreach(var pair in _userManager)
        {
            if(pair.Key != user.Email && pair.Value == UserStatus.Bidding)
            {
                return false;
            }
        }

        return true;
    }

    private void NewRound()
    {
        InitUserStatuses();

        CreateRound();

        // Broadcast a particular user to pick a player.
        var picker = GetSelectingUser();
        OnUserPicking?.Invoke(picker);
    }

    public void CreateRound()
    {
        _roundBuilder.AddRoundNo();
        _roundBuilder.AddTimer(_settings.TimerMinutes);
        _round = _roundBuilder.GetRound();

        _round.OnTickOccurred += OnTickOccurred;
        _round.OnTimerFinished += OnTimerFinished;
        _round.OnBiddingStarted += OnBiddingStarted;
        _round.OnPlayerUnsold += OnPlayerUnsold;

    }

    private void OnPlayerUnsold(Player picked)
    {
        _playerManager[picked.Id] = PlayerStatus.Unsold;

        // Start new round
        NewRound();
    }

    public void PlayerPicked(Player player)
    {
        _round.Picked(player);
    }

    public void Bid(User user, int amount)
    {
        var bidSuccessful = _round.Bid(user,amount);

        if(bidSuccessful && OtherHandsDown(user))
        {
            // Bid has been player and current 
            // player has been sold.



        }


    }

    public void UserOut(User user)
    {
        if(!_userManager.ContainsKey(user.Email))
        {
            throw new KeyNotFoundException();
        }

        _userManager[user.Email] = UserStatus.Out;

        if(AllUsersOut)
        {
            _round.AllHandsDown();
        }
    }

    public User GetSelectingUser()
    {
        var round = _round.GetCurrentRound();
        return _users[round % _users.Count];
    }

    public void OnTimerFinished()
    {
        var user = _round.GetCurrentWinner();
        var picked = _round.GetCurrentPlayer();

        if(user is null)
        {
            // The player is unsold
            _playerManager[picked.Id] = PlayerStatus.Unsold;
            return;
        }

        // The player has been sold
        _squadManager.SpendAmount(user, _round.GetHighestBid());
        _playerManager[picked.Id] = PlayerStatus.Sold;

    }


}
