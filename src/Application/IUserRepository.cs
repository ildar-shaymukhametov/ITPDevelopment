using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Application;

public interface IUserRepository
{
    public Task<User?> GetByIdAsync(int id);
    public Task<List<User>> GetAllAsync();
}
