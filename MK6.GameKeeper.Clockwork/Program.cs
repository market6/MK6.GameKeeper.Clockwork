using Serilog;
using System.Configuration;
using Topshelf;

namespace MK6.GameKeeper.Clockwork
{
    class Program
    {
        const string DefaultServiceName = "MK6.Clockwork";

        const string DefaultDisplayName = "MK6.Clockwork";

        const string DefaultDescription = "Scheduled task manager";

        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.AppSettings()
                .CreateLogger();

            HostFactory.Run(config =>
            {
                config.Service<Scheduler>(
                    host => new Scheduler(ConfigurationManager.AppSettings),
                    svc =>
                    {
                        svc.BeforeStartingService(() => Log.Information("Starting service"));
                        svc.AfterStartingService(() => Log.Information("Service started"));
                        svc.BeforeStoppingService(() => Log.Information("Stopping service"));
                        svc.AfterStoppingService(() => Log.Information("Service stopped"));
                    });

                config.RunAsLocalService();
                config.SetServiceName(ConfigurationManager.AppSettings["Name"] ?? DefaultServiceName);
                config.SetDisplayName(ConfigurationManager.AppSettings["DisplayName"] ?? DefaultDisplayName);
                config.SetDescription(ConfigurationManager.AppSettings["Description"] ?? DefaultDescription);
            });

        }
    }
}
