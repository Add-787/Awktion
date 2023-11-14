using Awktion.Domain.Entities.Games.Handlers;
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

    public Game(Settings settings,IList<User> users)
    {
        _settings = settings;
        _users = users;
        _roundBuilder = new();
        _budgetManager = new BudgetManager(_settings.Budget,users);
    }

    public void StartGame()
    {
        _roundBuilder.AddRoundNo();
        _roundBuilder.AddTimer(1);

        _round = _roundBuilder.GetRound();

        _round.OnTickOccurred += OnTickOccurred;
    }


}
