using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    // Temporary in memory implementation using a List
    private static readonly List<User> _usersList = new();

    public void Add(User user)
    {
        _usersList.Add(user);
    }

    public User? GetUserByEmail(string email)
    {
        return _usersList.SingleOrDefault(u => u.Email == email);
    }
}
