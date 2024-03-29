﻿using System;
using System.Linq;
using System.Threading;
using System.Windows;
using Aluetjen.Infrastructure;
using Aluetjen.Jira.Contexts;
using Aluetjen.Jira.Contexts.PublicEvents;
using Funq;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;

namespace Aluetjen.JiraSync
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        private static volatile bool _classInitialized;

        /// <remarks>
        /// ScheduledAgent constructor, initializes the UnhandledException handler
        /// </remarks>
        public ScheduledAgent()
        {
            if (!_classInitialized)
            {
                _classInitialized = true;
                // Subscribe to the managed exception handler
                Deployment.Current.Dispatcher.BeginInvoke(delegate
                {
                    Application.Current.UnhandledException += ScheduledAgent_UnhandledException;
                });
            }
        }

        /// Code to execute on Unhandled Exceptions
        private void ScheduledAgent_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        /// <summary>
        /// Agent that runs a scheduled task
        /// </summary>
        /// <param name="task">
        /// The invoked task
        /// </param>
        /// <remarks>
        /// This method is called when a periodic or resource intensive task is invoked
        /// </remarks>
        protected override void OnInvoke(ScheduledTask task)
        {
            Config.Configure();

            var bus = Config.Container.Resolve<IBus>();

            bus.Publish(new ApplicationLoadedEvent{IsAgent = true});
            
            var mainTile = ShellTile.ActiveTiles.First();
            mainTile.Update(new StandardTileData
            {
                Count = 1
            });

            Thread.Sleep(TimeSpan.FromMinutes(5));

            NotifyComplete();
        }
    }
}