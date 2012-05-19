using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Aluetjen.Jira.Contexts.Review.Mvvm.ViewModel;
using Aluetjen.Jira.Infrastructure;

namespace Aluetjen.Jira.Contexts.Review.Mvvm
{
    public partial class IssueListControl : UserControl
    {
        public IssueListControl()
        {
            InitializeComponent();

            var dataContext = new AllIssues{Store = Config.Container.Resolve<IDocumentStore>()};
            dataContext.Load();
            
            DataContext = dataContext;
        }

        private void IssuesListBox_Tap(object sender, GestureEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
