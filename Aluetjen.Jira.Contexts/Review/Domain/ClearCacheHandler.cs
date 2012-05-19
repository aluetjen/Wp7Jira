using Aluetjen.Jira.Contexts.Review.Mvvm.ViewModel;
using Aluetjen.Jira.Contexts.Settings.Events;

namespace Aluetjen.Jira.Contexts.Review.Domain
{
    public class ClearCacheHandler : IHandleMessages<ClearCacheCommand>
    {
        public IDocumentStore DocumentStore { get; set; }

        public void Handle(ClearCacheCommand message)
        {
            DocumentStore.DeleteAll<Issue>();
        }
    }
}
