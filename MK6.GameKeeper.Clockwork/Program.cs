using Serilog;
using System;
using System.Threading;

namespace MK6.GameKeeper.Clockwork
{
    class Program
    {
        static void Main(string[] args)
        {
            var pluginName = args[0];

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.AppSettings()
                .CreateLogger();

            var sched = new Service();
            var quitSemaphore = new Semaphore(0, 1);

            Console.CancelKeyPress += delegate
            {
                Log.Information("Stopping " + pluginName);
                sched.Stop();
                Log.Information(pluginName + " stopped");

                quitSemaphore.Release();
            };

            Log.Information("Starting " + pluginName);
            sched.Start();
            Log.Information(pluginName + " started");

            quitSemaphore.WaitOne();
        }
    }
}
