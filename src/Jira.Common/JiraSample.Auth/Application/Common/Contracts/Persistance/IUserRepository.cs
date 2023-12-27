using JiraSample.Auth.Entities;

namespace JiraSample.Auth.Application.Common.Contracts.Persistance;

public interface IUserRepository
{
    User GetUserByEmail(string email);
    void Add(User user);
}
