using System;
using Aluetjen.Jira.Infrastructure;

namespace Aluetjen.Jira.Contexts.Tracking.Events
{
    public class NewActivityEvent : Message
    {
        public string Key { get; set; }
        public string Description { get; set; }
        public DateTime PostedOn { get; set; }
    }
}
