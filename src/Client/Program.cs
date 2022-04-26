using System.Windows.Forms;
using App = System.Windows.Forms.Application;
using Microsoft.Extensions.Hosting;
using Infrastructure;

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
                })
                .Build();

            App.SetHighDpiMode(HighDpiMode.SystemAware);
            App.EnableVisualStyles();
            App.SetCompatibleTextRenderingDefault(false);

            App.Run(new Form1());
        }
    }
}
