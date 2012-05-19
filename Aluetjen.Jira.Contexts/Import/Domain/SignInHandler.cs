using Aluetjen.Jira.Contexts.Import.Events;

namespace Aluetjen.Jira.Contexts.Import.Domain
{
    public class SignInCommandHandler : IHandleMessages<SignInCommand>
    {
        public IBus Bus { get; set; }

        public void Handle(SignInCommand message)
        {
            Bus.Publish(new LoggedInEvent
                            {
                                Password = message.Password,
                                Url = message.Url,
                                UserName = message.UserName
                            });
        }
    }
}
