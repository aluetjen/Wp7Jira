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
using Aluetjen.Jira.Contexts.Import.Events;
using Microsoft.Phone.Controls;

namespace Aluetjen.Jira.Contexts.Import.Mvvm
{
    public class SignInActivator : IHandleMessages<UnauthorizedNotLoggedInEvent>
    {
        public PhoneApplicationFrame RootFrame;

        public void Handle(UnauthorizedNotLoggedInEvent message)
        {
            RootFrame.Navigate(new Uri("/Contexts/Import/Mvvm/SignInPage.xaml", UriKind.Relative));
        }
    }
}
