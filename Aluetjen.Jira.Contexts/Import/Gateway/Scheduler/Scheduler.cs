using System;
using System.Windows;
using Microsoft.Phone.Scheduler;

namespace Aluetjen.Jira.Contexts.Import.Gateway.Scheduler
{
    public class Scheduler : IScheduler
    {
        private const string TaskName = "JiraSync";

        public void ScheduleSync()
        {
            // Obtain a reference to the period task, if one exists
            var periodicTask = ScheduledActionService.Find(TaskName) as ResourceIntensiveTask;

            // If the task already exists and background agents are enabled for the
            // application, you must remove the task and then add it again to update 
            // the schedule
            if (periodicTask != null)
            {
                RemoveAgent(TaskName);
            }

            periodicTask = new ResourceIntensiveTask(TaskName)
                               {
                                   Description = "Synchronization of new JIRA issues"
                               };

            // Place the call to Add in a try block in case the user has disabled agents.
            try
            {
                ScheduledActionService.Add(periodicTask);

                // If debugging is enabled, use LaunchForTest to launch the agent in one minute.
#if(DEBUG_AGENT)
    ScheduledActionService.LaunchForTest(TaskName, TimeSpan.FromSeconds(60));
#endif
            }
            catch (InvalidOperationException exception)
            {
                if (exception.Message.Contains("BNS Error: The action is disabled"))
                {
                    MessageBox.Show("Background agents for this application have been disabled by the user.");
                }

                if (exception.Message.Contains("BNS Error: The maximum number of ScheduledActions of this type have already been added."))
                {
                    // No user action required. The system prompts the user when the hard limit of periodic tasks has been reached.
                }
            }
            catch (SchedulerServiceException)
            {
                // No user action required.
            }
        }

        public void ClearSchedule()
        {
            RemoveAgent(TaskName);
        }

        private static void RemoveAgent(string name)
        {
            try
            {
                ScheduledActionService.Remove(name);
            }
            catch (Exception)
            {
            }
        }
    }
}