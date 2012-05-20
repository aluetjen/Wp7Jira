using Aluetjen.Jira.Contexts.Import.Events;

namespace Aluetjen.Jira.Contexts.Review.ViewModel
{
    public class IssueIssueUpdatedHandler : IHandleMessages<IssueUpdatedEvent>
    {
        public IDocumentStore Store { get; set; }

        public void Handle(IssueUpdatedEvent message)
        {
            Issue issue;
            if (!Store.TryLoad(message.Key, out issue))
            {
                issue = new Issue {Key = message.Key};
            }

            issue.Summary = message.Summary;

            Store.Store(issue);
        }
    }
}