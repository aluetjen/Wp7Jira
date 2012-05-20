using System;
using Aluetjen.Jira.Contexts.Import.Events;
using Aluetjen.Jira.Contexts.Import.Gateway.Jira;
using Aluetjen.Jira.Contexts.Import.Gateway.Scheduler;
using Aluetjen.Jira.Contexts.PublicEvents;

namespace Aluetjen.Jira.Contexts.Import.Domain
{
    public class SyncHandler : IHandleMessages<ApplicationLoadedEvent>, IHandleMessages<LoggedInEvent>
    {
        public IJiraService Jira { get; set; }
        public IScheduler Scheduler { get; set; }

        public IBus Bus { get; set; }

        public void Handle(ApplicationLoadedEvent message)
        {
            DiscoverProjects();

            if (!message.IsAgent)
            {
                Scheduler.ScheduleSync();
            }
        }

        public void Handle(LoggedInEvent message)
        {
            DiscoverProjects();
        }

        private void DiscoverProjects()
        {
            try
            {
                Jira.Get<Project[]>("project", r => Bus.Publish(new ProjectsDiscoveredEvent { Projects = r }));
            }
            catch (UnauthorizedAccessException)
            {
                Bus.Publish(new UnauthorizedNotLoggedInEvent());
            }
        }
    }
}
