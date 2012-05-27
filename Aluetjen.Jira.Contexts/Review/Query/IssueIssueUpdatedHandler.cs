using Aluetjen.Infrastructure;
using Aluetjen.Jira.Contexts.Import.Events;
using Aluetjen.Jira.Contexts.Review.Query;

namespace Aluetjen.Jira.Contexts.Review.ViewModel
{
    public class IssueIssueUpdatedHandler : IHandleUiMessage<IssueUpdatedEvent>
    {
        public IDocumentStore Store { get; set; }

        public void Handle(IssueUpdatedEvent message)
        {
            var issue = Store.LoadOrCreate<Issue>(message.Key);

            issue.Summary = message.Summary;

            Store.Store(issue);
        }
    }
}