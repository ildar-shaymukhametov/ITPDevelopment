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
                    new User
                    {
                        FirstName = "Sam",
                        LastName = "Brave",
                        Users = new List<User>
                        {
                            new User
                            {
                                FirstName = "Gandalf",
                                LastName = "Grey"
                            }
                        }
                    },
                    new User
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