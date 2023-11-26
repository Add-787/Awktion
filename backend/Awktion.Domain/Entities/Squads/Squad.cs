using System.Linq;

namespace Awktion.Domain.Entities.Squads;

public class Squad
{
    private readonly Dictionary<int,int> _bids;
    public int budget { get; set; }
    public Squad(int total)
    {
        _bids = new();
        budget = total;
    }

    public void AddPlayer(int playerId, int price)
    {
        _bids[playerId] = price;
    }

    public IList<(int,int)> GetPlayers()
    {
        return _bids.Select((kv) => (kv.Key,kv.Value)).ToList();
    }
}
