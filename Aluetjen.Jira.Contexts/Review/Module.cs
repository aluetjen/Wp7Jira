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
using Aluetjen.Jira.Contexts.Import.Domain;
using Aluetjen.Jira.Contexts.Import.Events;
using Aluetjen.Jira.Contexts.Review.ViewModel;
using Aluetjen.Jira.Contexts.Tracking.Events;
using Aluetjen.Jira.Infrastructure;
using Funq;

namespace Aluetjen.Jira.Contexts.Review
{
    public class Module
    {
        public static void Confiure(Container container)
        {
            container.Register(c => new IssueIssueUpdatedHandler {Store = c.Resolve<IDocumentStore>()});
            container.Register(c => new ReviewNewActivityHandler {Store = c.Resolve<IDocumentStore>()});

            var bus = container.Resolve<IBus>();

            bus.RegisterHandler<IssueIssueUpdatedHandler, IssueUpdatedEvent>();
            bus.RegisterHandler<ReviewNewActivityHandler, NewActivityEvent>();
        }
    }
}
