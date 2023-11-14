namespace Awktion.Domain.Entities.Games;

/// <summary>
/// This class manages the budget of each user in the game.
/// It is a dictionary with an user email being the key. 
/// </summary>
public class BudgetManager
{
    private readonly Dictionary<string,int> _budgets;
    public BudgetManager(int budget,IList<User> users)
    {
        _budgets = new();
        foreach(User u in users)
        {
            _budgets[u.Email] = budget;
        }
    }

    public bool HasBudget(User u, int amount)
    {
        return _budgets[u.Email] >= amount;
    }

    public bool SpendAmount(User u, int amount)
    {
        if(!HasBudget(u,amount))
        {
            return false;
        }

        _budgets[u.Email] -= amount;
        return true;
    }
}
