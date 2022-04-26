using System.Collections.Generic;
using Domain;

namespace Application;

public interface IUserRepository
{
    public Task<List<User>> GetByIdAsync(int id);
    public Task<List<User>> GetAllAsync();
}
