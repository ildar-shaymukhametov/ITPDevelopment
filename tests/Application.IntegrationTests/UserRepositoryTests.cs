using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Infrastructure;
using Xunit;

namespace Application.IntegrationTests;

public class UserRepositoryTests
{
    [Fact]
    public async Task GetById__Finds_user_by_id()
    {
        var user = new User { FirstName = "Foo" };

        using var factory = new ApplicationDbContextFactory();
        using (var context = factory.CreateContext())
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
        }

        using (var context = factory.CreateContext())
        {
            var sut = new UserRepository(context);
            var actual = await sut.GetByIdAsync(user.Id);

            Assert.Equal(user.FirstName, actual?.FirstName);
        }
    }

    [Fact]
    public async Task GetById__Loads_users_tree()
    {
        var firstName = "Foo";
        var user = new User
        {
            Users = new List<User>
            {
                new() { FirstName = firstName }
            }
        };

        using var factory = new ApplicationDbContextFactory();
        using (var context = factory.CreateContext())
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
        }

        using (var context = factory.CreateContext())
        {
            var sut = new UserRepository(context);
            var actual = await sut.GetByIdAsync(user.Id);

            Assert.Collection(actual.Users, x => Assert.Equal(firstName, x.FirstName));
        }
    }

    [Fact]
    public async Task GetAll__Returns_all_users()
    {
        var userA = new User { FirstName = "Foo" };
        var userB = new User { FirstName = "Bar" };

        using var factory = new ApplicationDbContextFactory();
        using (var context = factory.CreateContext())
        {
            context.Users.Add(userA);
            context.Users.Add(userB);
            await context.SaveChangesAsync();
        }

        using (var context = factory.CreateContext())
        {
            var sut = new UserRepository(context);
            var actual = await sut.GetAllAsync();

            Assert.Collection(actual,
                x => Assert.Equal(userA.FirstName, x.FirstName),
                x => Assert.Equal(userB.FirstName, x.FirstName));
        }
    }
}