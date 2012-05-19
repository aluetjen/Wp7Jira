using Aluetjen.Jira.Infrastructure;

namespace Aluetjen.Jira.Contexts.Import.Events
{
    public class IssueUpdatedEvent : Message
    {
        public string Key { get; set; }
        public string Self { get; set; }
        public string Summary { get; set; }
    }
}