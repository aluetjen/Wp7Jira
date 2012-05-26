using Aluetjen.Infrastructure;
using Aluetjen.Jira.Contexts.Import.Domain;
using Aluetjen.Jira.Contexts.Import.Events;
using Aluetjen.Jira.Contexts.Import.Gateway.Jira;
using Aluetjen.Jira.Contexts.Import.Gateway.Scheduler;
using Aluetjen.Jira.Contexts.Import.ViewModel;
using Aluetjen.Jira.Contexts.PublicEvents;
using Funq;

namespace Aluetjen.Jira.Contexts.Import
{
    public class Module
    {
        public static void Configure(Container container)
        {
            container.Register<IJiraService>(c => new JiraService{Store = c.Resolve<IDocumentStore>(), Bus = c.Resolve<IBus>()});
            container.Register<IScheduler>(c => new Scheduler());

            container.Register(c => new SignInCommandHandler{Bus = c.Resolve<IBus>()});
            container.Register(
                c => new SyncHandler
                         {
                             Bus = c.Resolve<IBus>(),
                             Jira = c.Resolve<IJiraService>(),
                             Scheduler = c.Resolve<IScheduler>()
                         });
            container.Register(
                c => new SyncProjectHandler
                    {
                        Bus = c.Resolve<IBus>(),
                        Store = c.Resolve<IDocumentStore>(),
                        Jira = c.Resolve<IJiraService>()
                    });
            container.Register(c => new SyncNewlyDiscoveredProjectsByDefaultHandler {Bus = c.Resolve<IBus>()});

            container.Register(c => new ProfileLoggedInEventHandler { Store = c.Resolve<IDocumentStore>() });

            var bus = container.Resolve<IBus>();

            bus.RegisterHandler<SignInCommandHandler, SignInCommand>();
            bus.RegisterHandler<SyncHandler, ApplicationLoadedEvent>();
            bus.RegisterHandler<ProfileLoggedInEventHandler, LoggedInEvent>();
            bus.RegisterHandler<SyncHandler, LoggedInEvent>();
            bus.RegisterHandler<SyncProjectHandler, ProjectsDiscoveredEvent>();
            bus.RegisterHandler<SyncProjectHandler, SyncProjectCommand>();
            bus.RegisterHandler<SyncNewlyDiscoveredProjectsByDefaultHandler, DiscoveredNewProjectEvent>();
        }
    }
}
