using System.Collections.Concurrent;
using Awktion.Domain.Models;

namespace Awktion.Domain.Users;

public class UserHandler
{
    private ConcurrentDictionary<string,User> _userList;

    public UserHandler()
    {
        _userList = new ConcurrentDictionary<string, User>();
    }
}
