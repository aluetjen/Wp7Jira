using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Aluetjen.Jira.Contexts.Import.ViewModel;

namespace Aluetjen.Jira.Contexts.Import.Mvvm
{
    public partial class ProjectsControl : UserControl
    {
        public ProjectsControl()
        {
            InitializeComponent();

            Loaded += new RoutedEventHandler(ProjectsControl_Loaded);
        }

        void ProjectsControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new AllProjectsViewModel
                              {
                                  Projects = new ObservableCollection<Project>
                                                 {
                                                     new Project{Id = 1000, Key = "PROD", Name = "Production Support"},
                                                     new Project{Id = 1000, Key = "DE", Name = "Int DE"},
                                                     new Project{Id = 1000, Key = "UK", Name = "Int UK"},
                                                     new Project{Id = 1000, Key = "INF", Name = "Infrastructure"},
                                                     new Project{Id = 1000, Key = "SUP", Name = "Helpdesk"}
                                                 }
                              };
        }
    }
}
