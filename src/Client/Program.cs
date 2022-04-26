using System.Windows.Forms;
using App = System.Windows.Forms.Application;
using Microsoft.Extensions.Hosting;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Application;

namespace Client
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
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

            App.Run(new Form1(
                host.Services.GetRequiredService<ILogger<Form1>>(),
                host.Services.GetRequiredService<IUserRepository>()
            ));
        }
    }
}
