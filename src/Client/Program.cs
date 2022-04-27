using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using App = System.Windows.Forms.Application;
using Microsoft.Extensions.Hosting;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Application;
using Microsoft.EntityFrameworkCore;

namespace Client;

class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostBuilder, services) =>
            {
                services.AddInfrastructure(hostBuilder.Configuration);
                services.AddTransient<IUserRepository, UserRepository>();
            })
            .Build();

        App.SetHighDpiMode(HighDpiMode.SystemAware);
        App.EnableVisualStyles();
        App.SetCompatibleTextRenderingDefault(false);

        try
        {
            var dbContext = host.Services.GetRequiredService<ApplicationDbContext>();
            await dbContext.Database.OpenConnectionAsync();
            await dbContext.Database.EnsureCreatedAsync();
            await ApplicationDbContextSeed.SeedSampleDataAsync(dbContext);
            await dbContext.Database.CloseConnectionAsync();
        }
        catch (Exception ex)
        {
            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while migrating or seeding the database.");
            throw;
        }

        App.Run(new Form1(
            host.Services.GetRequiredService<ILogger<Form1>>(),
            host.Services.GetRequiredService<IUserRepository>()
        ));
    }
}