using Aluetjen.Jira.Infrastructure;

namespace Aluetjen.Jira.Contexts.Import.Domain
{
    public class SignInCommand : Message
    {
        public string Url { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
