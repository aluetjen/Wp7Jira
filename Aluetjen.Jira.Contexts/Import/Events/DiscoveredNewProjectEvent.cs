using Aluetjen.Jira.Contexts.Import.Gateway;
using Aluetjen.Jira.Contexts.Import.Gateway.Jira;
using Aluetjen.Jira.Infrastructure;

namespace Aluetjen.Jira.Contexts.Import.Events
{
    public class DiscoveredNewProjectEvent : Message
    {
        public Project Project { get; set; }
    }

    public class SyncProjectCommand : Message
    {
        public Project Project { get; set; }
        public bool IsBackgroundAgentSync { get; set; }
    }
}