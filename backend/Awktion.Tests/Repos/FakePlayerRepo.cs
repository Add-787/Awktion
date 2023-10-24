using Awktion.Domain.Models;
using Awktion.Domain.Repos;

namespace Awktion.Tests.Repos;

/// <summary>
/// Player Repository with default players for testing purposes.
/// </summary>
public class FakePlayerRepo : Repo<Player>
{
    private readonly IList<Player> Players = new List<Player>
    {
        new() { Id=1, Name = "Lionel Messi", Age = 34, BasePrice = 15, Country = "Argentina", Pos = Position.FW },
        new() { Id=2, Name = "Cristiano Ronaldo", Age = 38, BasePrice = 10, Country = "Portugal", Pos = Position.FW },
        new() { Id=3, Name = "Sergio Ramos", Age = 34, BasePrice = 5, Country = "Spain", Pos = Position.DEF },
        new() { Id=4, Name = "Andres Iniesta", Age = 39, BasePrice = 7, Country = "Spain", Pos = Position.MID },
        new() { Id=5, Name = "Manuel Neuer", Age = 35, BasePrice = 8, Country = "Germany", Pos = Position.GK }
    }; 
    
    public void Add(Player player)
    {
        if(Players.Where(p => p.Id == player.Id).Any())
        {
            throw new AlreadyExistsException<Player>();
        }

        Players.Add(player);
    }

    public Player Get(int Id)
    {
        return Players.FirstOrDefault(p => p.Id == Id) ?? throw new NotFoundException<Player>();
    }

    public IList<Player> GetAll()
    {
        return Players ?? throw new Exception();
    }

    public void Update(Player t)
    {
        throw new NotImplementedException();
    }

}
