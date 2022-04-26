using System.Collections.Generic;
using Domain;

namespace Application;

public interface IUserRepository
{
    public Task<User?> GetByIdAsync(int id);
    public Task<List<User>> GetAllAsync();
}
