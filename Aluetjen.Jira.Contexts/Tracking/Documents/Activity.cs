using System;
using Aluetjen.Jira.Infrastructure;

namespace Aluetjen.Jira.Contexts.Tracking.Documents
{
    public class Activity : IDocument
    {
        public string Key { get; set; }
        public int Version { get; set; }

        public string Description { get; set; }
        public DateTime PostedOn { get; set; }
    }
}
