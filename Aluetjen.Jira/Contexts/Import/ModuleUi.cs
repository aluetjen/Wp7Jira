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
using Aluetjen.Jira.Contexts.Import.Gateway;
using Aluetjen.Jira.Contexts.Import.Gateway.Scheduler;
using Aluetjen.Jira.Contexts.Import.Mvvm;
using Aluetjen.Jira.Contexts.Import.Mvvm.ViewModel;
using Aluetjen.Jira.Contexts.PublicEvents.Infrastructure;
using Funq;
using Microsoft.Phone.Controls;

namespace Aluetjen.Jira.Contexts.Import
{
    public class ModuleUi
    {
        public static void Configure(Container container, PhoneApplicationFrame root)
        {
            container.Register<IJiraService>(c => new JiraService { Store = c.Resolve<IDocumentStore>() });
            container.Register<IScheduler>(c => new Scheduler());

            container.Register(c => new SignInCommandHandler { Bus = c.Resolve<IBus>() });
            container.Register(c => new SyncHandler { Bus = c.Resolve<IBus>(), Jira = c.Resolve<IJiraService>() });
            container.Register(c => new SyncProjectHandler { Bus = c.Resolve<IBus>(), Store = c.Resolve<IDocumentStore>(), Jira = c.Resolve<IJiraService>(), Scheduler = c.Resolve<IScheduler>() });
            container.Register(c => new SyncNewlyDiscoveredProjectsByDefaultHandler { Bus = c.Resolve<IBus>() });

            container.Register(c => new ProfileLoggedInEventHandler { Store = c.Resolve<IDocumentStore>() });

            container.Register(c => new SignInActivator { RootFrame = root });

            var bus = container.Resolve<IBus>();

            bus.RegisterHandler<SignInCommandHandler, SignInCommand>();
            bus.RegisterHandler<SyncHandler, ApplicationLoadedEvent>();
            bus.RegisterHandler<ProfileLoggedInEventHandler, LoggedInEvent>();
            bus.RegisterHandler<SyncHandler, LoggedInEvent>();
            bus.RegisterHandler<SyncProjectHandler, ProjectsDiscoveredEvent>();
            bus.RegisterHandler<SyncProjectHandler, SyncProjectCommand>();
            bus.RegisterHandler<SignInActivator, UnauthorizedNotLoggedInEvent>();
            bus.RegisterHandler<SyncNewlyDiscoveredProjectsByDefaultHandler, DiscoveredNewProjectEvent>();
        }
    }
}
