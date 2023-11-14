using Awktion.Domain.Entities.Games.Handlers;
using Awktion.Domain.Entities.Players;
using Awktion.Domain.Entities.Rounds;

namespace Awktion.Domain.Entities.Games;

public class Game
{
    private readonly Settings _settings;
    private readonly IList<User> _users;
    private Round _round;
    private readonly RoundBuilder _roundBuilder;
    private readonly BudgetManager _budgetManager;


    public event TickOccurredEventHandler OnTickOccurred;
    public event Action<User> OnUserPicking;

    private Dictionary<int,PlayerStatus> _playerStatuses;

    public Game(Settings settings,List<User> users)
    {
        _settings = settings;
        _users = users;
        _roundBuilder = new();
        _budgetManager = new BudgetManager(_settings.Budget,users);
        InitStatuses();
    }

    public void InitStatuses()
    {
        _playerStatuses = new();
        _settings.AvailablePlayerIds.ForEach((id) => {
            _playerStatuses.Add(id, PlayerStatus.Waiting);
        });
    }

    public void Start()
    {
        CreateRound();

        // Broadcast a particular user to pick a player.
        var picker = GetSelectingUser();
        OnUserPicking?.Invoke(picker);

    }

    public void CreateRound()
    {
        _roundBuilder.AddRoundNo();
        _roundBuilder.AddTimer(1);
        _round = _roundBuilder.GetRound();

        _round.OnTickOccurred += OnTickOccurred;
        _round.OnTimerFinished += OnTimerFinished;

    }

    public void PlayerPicked(Player player)
    {
        _round.Picked(player);

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
            _playerStatuses[picked.Id] = PlayerStatus.Unsold;
            return;
        }

        // The player has been sold
        _budgetManager.SpendAmount(user, _round.GetHighestBid());
        _playerStatuses[picked.Id] = PlayerStatus.Sold;



    }



    




}
