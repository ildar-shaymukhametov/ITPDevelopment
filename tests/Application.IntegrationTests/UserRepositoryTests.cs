using Domain;
using Infrastructure;
using Xunit;

namespace Application.IntegrationTests;

public class UserRepositoryTests
{
    public UserRepositoryTests()
    {
        
    }

    [Fact]
    public async Task GetById__Finds_user_by_id()
    {
        var firstName = "Foo";
        var userId = 0;
        
        using var factory = new ApplicationDbContextFactory();
        using (var context = factory.CreateContext())
        {
            await context.Database.EnsureCreatedAsync();
        }

        using (var context = factory.CreateContext())
        {
            var user = new User { FirstName = firstName };
            context.Users.Add(user);
            await context.SaveChangesAsync();
            userId = user.Id;
        }

        using (var context = factory.CreateContext())
        {
            var sut = new UserRepository(context);
            var actual = await sut.GetByIdAsync(userId);

            Assert.Equal(firstName, actual?.FirstName);
        }
    }
}