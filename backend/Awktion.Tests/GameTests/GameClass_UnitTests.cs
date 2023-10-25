using System.ComponentModel;
using Awktion.Domain.Games;
using Awktion.Domain.Models;
using Awktion.Domain.Repos;
using Awktion.Tests.Repos;
using Microsoft.VisualBasic;

namespace Awktion.Tests.GameTests;

public class GameClass_UnitTests
{
    private readonly Game Game;
    private readonly Repo<Player> PlayerRepo;
    public GameClass_UnitTests()
    {
        PlayerRepo = new FakePlayerRepo();

        var settings = new GameSettings {
            AvailablePlayers = PlayerRepo.GetAll().ToList()
        };

        var users = new List<User>() {
            new() {
                Name = "fakeplayer1",
                Email = "fakeplayer1@email.com"
            },
            new() {
                Name = "fakeplayer2",
                Email = "fakeplayer2@email.com"
            }
        }; 

        Game = new Game(settings, users);
    }

    [Fact]
    public async void IsGame_PlayerPicked_EventRaised()
    {
        var eventCompleted = new TaskCompletionSource<bool>();

        Game.BiddingStarted += (t, e) =>
        {
            eventCompleted.SetResult(true);
        };

        Game.Start();

        var somePlayer = PlayerRepo.Get(1);
        Game.PlayerPicked(somePlayer);

        Assert.True(await eventCompleted.Task == true, $"Player: {somePlayer.Name} has been picked.");

    }


}
