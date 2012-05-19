using Aluetjen.Jira.Contexts;

namespace Aluetjen.Jira.Infrastructure
{
    public abstract class Handler
    {
        public IBus Bus { get; set; }
    }
}