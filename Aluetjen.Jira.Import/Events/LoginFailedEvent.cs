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
using Aluetjen.Jira.Infrastructure;

namespace Aluetjen.Jira.Contexts.Import.Events
{
    public class LoginFailedEvent : Message
    {
        public string Error { get; set; }
    }
}
