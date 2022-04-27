using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Infrastructure;

public static class ApplicationDbContextSeed
{
    public static async Task SeedSampleDataAsync(ApplicationDbContext context)
    {
        if (!context.Users.Any())
        {
            var userA = new User { FirstName = "A" };
            var userB = new User { FirstName = "B", Parent = userA };
            var userC = new User { FirstName = "C", Parent = userB };
            var userD = new User { FirstName = "D" };

            context.Users.AddRange(userA, userB, userC, userD);

            await context.SaveChangesAsync();
        }
    }
}