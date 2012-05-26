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
using Aluetjen.Infrastructure;
using Aluetjen.Jira.Contexts.PublicEvents;
using Aluetjen.Jira.Contexts.Tracking.Events;

namespace Aluetjen.Jira.Contexts.Tracking
{
    public class TestModeActivatedHandler : Handler, IHandleMessages<TestModeActivatedEvent>
    {
        public void Handle(TestModeActivatedEvent message)
        {
            Bus.Publish(new NewActivityEvent
                            {
                                Key = "TX-3928",
                                Description = "Test Ticket ",
                                PostedOn = DateTime.UtcNow,
                            });
        }
    }
}
