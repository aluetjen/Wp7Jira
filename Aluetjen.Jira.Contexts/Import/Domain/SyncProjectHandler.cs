using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Aluetjen.Jira.Contexts.Import.Domain.Sagas;
using Aluetjen.Jira.Contexts.Import.Events;
using Aluetjen.Jira.Contexts.Import.Gateway;
using Aluetjen.Jira.Contexts.Import.Gateway.Jira;
using Aluetjen.Jira.Contexts.Import.Gateway.Scheduler;
using Aluetjen.Jira.Infrastructure;

namespace Aluetjen.Jira.Contexts.Import.Domain
{
    public class SyncProjectHandler :
        IHandleMessages<ProjectsDiscoveredEvent>,
        IHandleMessages<SyncProjectCommand>
    {
        public IBus Bus { get; set; }
        public IDocumentStore Store { get; set; }
        
        public IJiraService Jira { get; set; }

        public void Handle(ProjectsDiscoveredEvent message)
        {
            foreach(var project in message.Projects)
            {
                if(!Store.Exists<SyncProjectSaga>(project.Key))
                {
                    Bus.Publish(new DiscoveredNewProjectEvent {Project = project});
                }
                else
                {
                    var syncSaga = Store.Load<SyncProjectSaga>(project.Key);

                    if (syncSaga.SyncEnabled) Bus.Publish(new SyncProjectCommand {Project = project});
                }
            }
        }

        public void Handle(SyncProjectCommand message)
        {
            if(!Store.Exists<SyncProjectSaga>(message.Project.Key))
            {
                var syncSaga = new SyncProjectSaga
                                   {
                                       Key = message.Project.Key,
                                       ProjectUrl = message.Project.Self,
                                       SyncEnabled = true
                                   };

                Store.Store(syncSaga);
            }

            Jira.Get<SearchResult>("search", r =>
                                                 {
                                                     foreach (var issue in r.Issues)
                                                     {
                                                         Bus.Publish(new IssueUpdatedEvent
                                                                         {
                                                                             Key = issue.Key,
                                                                             Self = issue.Self,
                                                                             Summary = issue.Fields.Summary
                                                                         });
                                                     }
                                                 });
        }
    }
}
