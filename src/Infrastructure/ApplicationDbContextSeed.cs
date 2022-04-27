using System.Collections.Generic;
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
            context.Users.Add(new User
            {
                FirstName = "Frodo",
                LastName = "Baggins",
                Users = new List<User>
                {
                    new()
                    {
                        FirstName = "Sam",
                        LastName = "Brave",
                        Users = new List<User>
                        {
                            new()
                            {
                                FirstName = "Gandalf",
                                LastName = "Grey"
                            }
                        }
                    },
                    new()
                    {
                        FirstName = "Aragorn",
                        LastName = "Numenor"
                    }
                }
            });

            await context.SaveChangesAsync();
        }
    }
}