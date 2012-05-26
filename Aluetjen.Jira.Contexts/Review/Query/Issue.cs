using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using Aluetjen.Infrastructure;
using Aluetjen.Jira.Contexts.Review.ViewModel;

namespace Aluetjen.Jira.Contexts.Review.Query
{
    public class Issue : IDocument, INotifyPropertyChanged
    {
        public Issue()
        {
            Comments = new ObservableCollection<Comment>();
        }

        public string Key { get; set; }

        private string  _summary;
        public string Summary
        {
            get { return _summary; }
            set
            {
                _summary = value;
                NotifyPropertyChanged("Summary");
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                NotifyPropertyChanged("Description");
            }
        }

        public ObservableCollection<Comment> Comments { get; set; }
       
        private string _givenWhenThen;
        public string GivenWhenThen
        {
            get { return _givenWhenThen; }
            set
            {
                _givenWhenThen = value;
                NotifyPropertyChanged("GivenWhenThen");
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
