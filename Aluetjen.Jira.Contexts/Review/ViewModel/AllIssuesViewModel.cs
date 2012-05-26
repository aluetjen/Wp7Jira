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
    public class AllIssuesViewModel : IHandleMessages<IssueUpdatedEvent>, INotifyPropertyChanged
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
            set
            {
                _issues = value;
                NotifyPropertyChanged("Issues");
            }
        }

        public void Load()
        {
            Issues = new ObservableCollection<Issue>(Store.LoadAll<Issue>());
        }

        public void Handle(IssueUpdatedEvent message)
        {
            var updatedIssue = Issues.FirstOrDefault(i => i.Key == message.Key);

            if(updatedIssue != null)
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
                Deployment.Current.Dispatcher.BeginInvoke(() => PropertyChanged(this, new PropertyChangedEventArgs(propertyName)));
            }
        }
    }
}
