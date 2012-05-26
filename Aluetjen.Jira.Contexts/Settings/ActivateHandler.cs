using Aluetjen.Infrastructure;
using Aluetjen.Jira.Contexts.PublicEvents;
using Aluetjen.Jira.Contexts.Settings.Events;

namespace Aluetjen.Jira.Contexts.Settings
{
    public class ActivateTestModeHandler : Handler, IHandleMessages<ActivateCommand>
    {
        public void Handle(ActivateCommand message)
        {
            Bus.Publish(new TestModeActivatedEvent());
        }
    }
}
