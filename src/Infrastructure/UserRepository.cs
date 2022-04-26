using Application;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public Task<List<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        var entity = await dbContext.Users.Include(x => x.Users).SingleOrDefaultAsync(x => x.Id == id);
        return entity;
    }
}
