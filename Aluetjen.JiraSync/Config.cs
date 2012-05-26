using System.Diagnostics;
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
using Aluetjen.Jira.Contexts;
using Aluetjen.Jira.Contexts.PublicEvents;
using Aluetjen.Jira.Contexts.Review;
using Aluetjen.Jira.Contexts.Settings;
using Aluetjen.Jira.Contexts.Settings.Events;
using Aluetjen.Jira.Contexts.Tracking;
using Aluetjen.Jira.Contexts.Tracking.Events;
using Funq;
using Microsoft.Phone.Controls;
using ClearCacheHandler = Aluetjen.Jira.Contexts.Settings.ClearCacheHandler;

// using Raven.Light.Impl;

namespace Aluetjen.JiraSync
{
    public class Config
    {
        public static Container Container = new Container();
        public static DocumentStore DocumentStore = new DocumentStore();

        public static void Configure()
        {
            Container.Register<IDocumentStore>(c => new DocumentStore());
            Container.Register<IBus>(c => new Bus(c));

            Container.Register(c => new ActivityNewActivityHandler { Bus = c.Resolve<IBus>(), DocumentStore = DocumentStore });
            Container.Register(c => new TestModeActivatedHandler { Bus = c.Resolve<IBus>() });
            Container.Register(c => new ActivateTestModeHandler { Bus = c.Resolve<IBus>() });
            Container.Register(c => new ClearCacheHandler { DocumentStore = c.Resolve<IDocumentStore>() });
            
            var bus = Container.Resolve<IBus>();

            bus.RegisterHandler<ActivityNewActivityHandler, NewActivityEvent>();
            bus.RegisterHandler<TestModeActivatedHandler, TestModeActivatedEvent>();
            bus.RegisterHandler<ActivateTestModeHandler, ActivateCommand>();

            Aluetjen.Jira.Contexts.Import.Module.Configure(Container);
            Aluetjen.Jira.Contexts.Review.Module.Confiure(Container);
        }
    }
}
