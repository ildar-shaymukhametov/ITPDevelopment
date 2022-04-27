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
        var firstName = "Foo";
        User user;

        using var factory = new ApplicationDbContextFactory();
        using (var context = factory.CreateContext())
        {
            user = new User { FirstName = firstName };
            context.Users.Add(user);
            await context.SaveChangesAsync();
        }

        using (var context = factory.CreateContext())
        {
            var sut = new UserRepository(context);
            var actual = await sut.GetByIdAsync(user.Id);

            Assert.Equal(firstName, actual?.FirstName);
        }
    }

    [Fact]
    public async Task GetById__Loads_users_tree()
    {
        var firstName = "Foo";
        User user;

        using var factory = new ApplicationDbContextFactory();
        using (var context = factory.CreateContext())
        {
            user = new User
            {
                Users = new List<User>
                {
                    new User { FirstName = firstName }
                }
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();
        }

        using (var context = factory.CreateContext())
        {
            var sut = new UserRepository(context);
            var actual = await sut.GetByIdAsync(user.Id);

            Assert.Collection(user.Users, x => Assert.Equal(firstName, x.FirstName));
        }
    }

    [Fact]
    public async Task GetAll__Returns_all_users()
    {
        var firstNameA = "Foo";
        var firstNameB = "Bar";
        User userA;
        User userB;

        using var factory = new ApplicationDbContextFactory();
        using (var context = factory.CreateContext())
        {
            userA = new User { FirstName = firstNameA };
            userB = new User { FirstName = firstNameB };
            context.Users.Add(userA);
            context.Users.Add(userB);
            await context.SaveChangesAsync();
        }

        using (var context = factory.CreateContext())
        {
            var sut = new UserRepository(context);
            var actual = await sut.GetAllAsync();

            Assert.Collection(actual,
                x => Assert.Equal(firstNameA, x.FirstName),
                x => Assert.Equal(firstNameB, x.FirstName));
        }
    }
}