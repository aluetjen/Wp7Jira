using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Aluetjen.Jira.Contexts.Import.ViewModel
{
    public class AllProjectsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Project> Projects { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
