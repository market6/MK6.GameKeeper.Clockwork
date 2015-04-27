using MK6.GameKeeper.AddIns;
using Quartz;
using Quartz.Impl;
using Serilog;
using System;
using System.Configuration;
using System.Reflection;

namespace MK6.GameKeeper.Clockwork
{
    // [AddIn("MK6.GameKeeper.Clockwork", Version="1.0")]
    public abstract class ClockworkService : GameKeeperAddIn
    {
        private readonly IScheduler scheduler;

        public ClockworkService()
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.AppSettings()
                .CreateLogger();

            var executingAssembly = ConfigurationManager.OpenExeConfiguration(
                Assembly.GetExecutingAssembly().Location);

            var schedulerFactory = new StdSchedulerFactory(ConfigurationManager.AppSettings);
            this.scheduler = schedulerFactory.GetScheduler();
        }

        public virtual void Start()
        {
            try
            {
                Log.Information("Starting scheduler");
                this.scheduler.Start();
                Log.Information("Scheduler started");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while starting scheduler");
            }
        }

        public virtual void Stop()
        {
            try
            {
                Log.Information("Shutting down scheduler");
                this.scheduler.Shutdown();
                Log.Information("Schduler shut down");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while shutting down scheduler");
            }
        }

        public virtual AddInStatus Status
        {
            get
            {
                return scheduler.IsStarted
                    ? AddInStatus.Running
                    : AddInStatus.Stopped;
            }
        }
    }
}
