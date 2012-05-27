using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Aluetjen.Infrastructure;
using Aluetjen.Jira.Contexts.Import.Events;
using Aluetjen.Jira.Contexts.Review.Query;

namespace Aluetjen.Jira.Contexts.Review.ViewModel
{
    public class AllIssuesViewModel : IHandleUiMessage<IssueUpdatedEvent>, INotifyPropertyChanged
    {
        public IDocumentStore Store { get; set; }

        public AllIssuesViewModel()
        {
            Issues = new ObservableCollection<Issue>();
        }

        private ObservableCollection<Issue> _issues;
        public ObservableCollection<Issue> Issues
        {
            get { return _issues; }
            set { _issues = value; }
        }

        public void Load()
        {
            foreach (var issue in Store.LoadAll<Issue>())
            {
                Issues.Add(issue);
            }
        }

        public void Handle(IssueUpdatedEvent message)
        {
            var updatedIssue = Issues.FirstOrDefault(i => i.Key == message.Key);

            if (updatedIssue != null)
            {
                updatedIssue.Summary = message.Summary;
            }
            else
            {
                var newIssue = Store.LoadOrCreate<Issue>(message.Key);
                Issues.Add(newIssue);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
