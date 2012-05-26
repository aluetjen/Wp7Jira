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
using Aluetjen.Jira.Contexts.Import.Domain;
using Aluetjen.Jira.Contexts.Import.Events;
using Aluetjen.Jira.Contexts.Import.Gateway;
using Aluetjen.Jira.Contexts.Import.Gateway.Scheduler;
using Aluetjen.Jira.Contexts.Import.Mvvm;
using Funq;
using Microsoft.Phone.Controls;

namespace Aluetjen.Jira.Contexts.Import
{
    public class ModuleUi
    {
        public static void Configure(Container container, PhoneApplicationFrame root)
        {
            container.Register(c => new SignInActivator { RootFrame = root });

            var bus = container.Resolve<IBus>();

            bus.RegisterHandler<SignInActivator, UnauthorizedNotLoggedInEvent>();
        }
    }
}
