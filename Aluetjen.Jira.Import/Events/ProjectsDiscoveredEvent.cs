using Aluetjen.Jira.Contexts.Import.Gateway;
using Aluetjen.Jira.Infrastructure;

namespace Aluetjen.Jira.Contexts.Import.Events
{
    public class ProjectsDiscoveredEvent : Message
    {
        public Project[] Projects { get; set; }
    }


}