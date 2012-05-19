using System;
using System.Collections.Generic;
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

namespace Aluetjen.Jira.Contexts.Tracking.Documents
{
    public class RecentActivities : IDocument
    {
        public RecentActivities()
        {
            Activities = new List<Activity>();
        }

        public string Key { get; set; }
        public IList<Activity> Activities { get; set; }
    }
}
