using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    public async Task<List<User>> GetAllAsync()
    {
        var result = await dbContext.Users
            .AsNoTracking()
            .Include(x => x.Users)
            .ToListAsync();

        return result;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        var result = await dbContext.Users
            .Include(x => x.Users)
            .SingleOrDefaultAsync(x => x.Id == id);

        return result;
    }

    public async Task UpdateAsync(UpdateUserModel model)
    {
        var user = await dbContext.Users.FindAsync(model.Id);
        if (user == null)
        {
            throw new Exception($"User with id {model.Id} not found");
        }

        dbContext.Entry(user).CurrentValues.SetValues(model);
        await dbContext.SaveChangesAsync();
    }
}
