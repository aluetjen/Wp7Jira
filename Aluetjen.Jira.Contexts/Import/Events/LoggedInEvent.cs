using Aluetjen.Infrastructure;

namespace Aluetjen.Jira.Contexts.Import.Events
{
    public class LoggedInEvent : Message
    {
        public string Url { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
