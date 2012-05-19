using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Aluetjen.Jira.Contexts.Import.Events;
using Aluetjen.Jira.Contexts.Import.Gateway;
using Aluetjen.Jira.Contexts.PublicEvents.Infrastructure;
using Aluetjen.Jira.Infrastructure;

namespace Aluetjen.Jira.Contexts.Import.Domain
{
    public class SyncHandler : IHandleMessages<ApplicationLoadedEvent>, IHandleMessages<LoggedInEvent>
    {
        public IJiraService Jira { get; set; }
        public IBus Bus { get; set; }

        public void Handle(ApplicationLoadedEvent message)
        {
            DiscoverProjects();
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
