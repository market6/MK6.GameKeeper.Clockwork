using Quartz;
using Quartz.Impl;
using Serilog;
using System;
using System.Configuration;

namespace MK6.GameKeeper.Clockwork
{
    class Service
    {
        private readonly IScheduler scheduler;

        public Service()
        {
            var schedulerFactory = new StdSchedulerFactory(ConfigurationManager.AppSettings);
            this.scheduler = schedulerFactory.GetScheduler();
        }

        public bool Start()
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
                return false;
            }

            return true;
        }

        public bool Stop()
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
                return false;
            }

            return true;
        }
    }
}
