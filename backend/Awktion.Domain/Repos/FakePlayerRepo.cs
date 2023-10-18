using Awktion.Domain.Models;

namespace Awktion.Domain.Repos;

public class FakePlayerRepo : Repo<Player>
{
    
    public void Add(Player t)
    {
        throw new NotImplementedException();
    }

    public Player Get(int Id)
    {
        throw new NotImplementedException();
    }

    public List<Player> GetAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Player t)
    {
        throw new NotImplementedException();
    }

}
