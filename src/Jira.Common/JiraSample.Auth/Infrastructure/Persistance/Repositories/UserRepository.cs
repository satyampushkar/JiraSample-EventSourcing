using JiraSample.Auth.Application.Common.Contracts.Persistance;
using JiraSample.Auth.Entities;

namespace JiraSample.Auth.Infrastructure.Persistance.Repositories;

public class UserRepository : IUserRepository
{
    private static readonly List<User> _users = new();
    public void Add(User user)
    {
        _users.Add(user);
    }

    public User GetUserByEmail(string email)
    {
        return _users.SingleOrDefault(u => u.Email == email);
    }
}
