using Awktion.Domain.Models;
using Awktion.Domain.Rooms;
using Awktion.Domain.Services;
using Awktion.Tests.Repos;
using Awktion.Tests.Services;

namespace Awktion.Tests.RoomTests;

public class RoomService_UnitTests
{
    private readonly IRoomService RoomService;

    public RoomService_UnitTests()
    {
        RoomService = new FakeRoomService(new FakeRoomRepo());
    }

    [Fact]
    public void IsRoomService_GettingRooms()
    {
        var room = RoomService.GetRoom(1);

        Assert.True(room != null);
    }

    [Fact]
    public void IsRoomService_AddingUserToRoom()
    {
        RoomService.AddUserToRoom(1, new User
        {
            Name = "fakeuser1",
            Email = "fakeuser1@email.com"
        });

        RoomService.AddUserToRoom(1, new User
        {
            Name = "fakeuser2",
            Email = "fakeuser2@email.com"
        });

        var count = RoomService.GetRoom(1).GetUsers().Count;

        Assert.True(count == 2, $"No.of users in room: {count}");
    }


}
