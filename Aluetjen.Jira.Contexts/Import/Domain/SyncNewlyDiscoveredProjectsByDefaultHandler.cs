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
using Aluetjen.Infrastructure;
using Aluetjen.Jira.Contexts.Import.Events;

namespace Aluetjen.Jira.Contexts.Import.Domain
{
    public class SyncNewlyDiscoveredProjectsByDefaultHandler : IHandleMessages<DiscoveredNewProjectEvent>
    {
        public IBus Bus { get; set; }

        public void Handle(DiscoveredNewProjectEvent message)
        {
            Bus.Publish(new SyncProjectCommand
                            {
                                Project = message.Project
                            });
        }
    }
}
