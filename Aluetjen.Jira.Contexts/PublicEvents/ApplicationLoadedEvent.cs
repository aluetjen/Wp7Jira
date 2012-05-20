using Aluetjen.Jira.Infrastructure;

namespace Aluetjen.Jira.Contexts.PublicEvents
{
    public class ApplicationLoadedEvent : Message
    {
        public bool IsAgent { get; set; }
    }
}
