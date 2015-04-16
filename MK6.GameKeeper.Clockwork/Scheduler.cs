using Quartz;
using Quartz.Impl;
using Serilog;
using System;
using System.Collections.Specialized;
using Topshelf;

namespace MK6.GameKeeper.Clockwork
{
    public class Scheduler : ServiceControl
    {
        private readonly IScheduler scheduler;

        public Scheduler(NameValueCollection properties)
        {
            var schedulerFactory = new StdSchedulerFactory(properties);
            this.scheduler = schedulerFactory.GetScheduler();
        }

        public bool Start(HostControl hostControl)
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

        public bool Stop(HostControl hostControl)
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
