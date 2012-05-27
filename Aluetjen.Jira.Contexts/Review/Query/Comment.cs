using System;
using System.ComponentModel;

namespace Aluetjen.Jira.Contexts.Review.ViewModel
{
    public class Comment : INotifyPropertyChanged
    {
        private DateTime _postedOn;
        public DateTime PostedOn
        {
            get { return _postedOn; }
            set
            {
                _postedOn = value;
                NotifyPropertyChanged("PostedOn");
            }
        }

        private string _user;
        public string User
        {
            get { return _user; }
            set { _user = value;NotifyPropertyChanged("User"); }
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