using Awktion.Domain.Entities.Players;
using Awktion.Domain.Entities.Users;

namespace Awktion.Domain.Entities.Squads;

public class SquadManager
{
    private readonly Dictionary<string,Squad> _squads = new();

    public SquadManager(int budget, IList<User> users)
    {
        InitKeyValues(budget, users);
    }

    public void InitKeyValues(int total, IList<User> users)
    {
        foreach(User u in users)
        {
            _squads[u.Email] = new Squad(total);
        }
    }

    public bool AddPlayerToSquad(User user, Player player, int price)
    {
        if(_squads.ContainsKey(user.Email))
        {
            _squads[user.Email].AddPlayer(player.Id,price);
            return true;
        }

        return false;
    }

    public bool HasBudget(User u,int amount)
    {
        return _squads[u.Email].budget >= amount;
    }

    public bool SpendAmount(User u, int amount)
    {
        if(!HasBudget(u,amount))
        {
            return false;
        }

        _squads[u.Email].budget -= amount;
        return true;
    }

}
