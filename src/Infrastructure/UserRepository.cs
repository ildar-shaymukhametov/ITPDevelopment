using Application;
using Domain;

namespace Infrastructure;

public class UserRepository : IUserRepository
{
    public Task<List<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<User>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
