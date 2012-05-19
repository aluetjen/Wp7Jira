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
using Aluetjen.Jira.Contexts.Settings.Events;
using Aluetjen.Jira.Contexts.Tracking.Documents;
using Aluetjen.Jira.Infrastructure;

namespace Aluetjen.Jira.Contexts.Settings
{
    public class ClearCacheHandler : IHandleMessages<ClearCacheCommand>
    {
        public IDocumentStore DocumentStore { get; set; }

        public void Handle(ClearCacheCommand message)
        {
            DocumentStore.DeleteAll<Activity>();
            DocumentStore.DeleteAll<RecentActivities>();
        }
    }
}
