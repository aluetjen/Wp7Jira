using Aluetjen.Jira.Contexts.Import.Events;
using Aluetjen.Jira.Infrastructure;

namespace Aluetjen.Jira.Contexts.Review.Mvvm.ViewModel
{
    public class IssueIssueUpdatedHandler : IHandleMessages<IssueUpdatedEvent>
    {
        public IDocumentStore Store { get; set; }

        public void Handle(IssueUpdatedEvent message)
        {
            var issue = new Issue
                            {
                                Key = message.Key,
                                Summary = message.Summary
                            };

            Store.Store(issue);
        }
    }
}