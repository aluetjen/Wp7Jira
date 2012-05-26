using System.Collections.Generic;
using System.Linq;
using Aluetjen.Infrastructure;

namespace Aluetjen.Jira.Contexts.Review.ViewModel
{
    public class AllIssues
    {
        public IDocumentStore Store { get; set; }

        public AllIssues()
        {
            Issues = new List<Issue>();
        }

        public IList<Issue> Issues { get; set; }

        public void Load()
        {
            Issues = Store.LoadAll<Issue>().ToList();
        }
    }
}
