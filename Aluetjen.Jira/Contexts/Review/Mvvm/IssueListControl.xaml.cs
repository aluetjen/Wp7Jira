using System;
using Aluetjen.Jira.Contexts.Review.ViewModel;
using Aluetjen.Jira.Infrastructure;
using Microsoft.Phone.Controls;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace Aluetjen.Jira.Contexts.Review.Mvvm
{
    public partial class IssueListControl
    {
        public IssueListControl()
        {
            InitializeComponent();

            var dataContext = new AllIssues{Store = Config.Container.Resolve<IDocumentStore>()};
            dataContext.Load();
            
            DataContext = dataContext;
        }

        private void IssueListBox_Tap(object sender, GestureEventArgs e)
        {
            var issue = IssueListBox.SelectedItem as Issue;

            if (issue != null)
            {
                var frame = App.Current.RootVisual as PhoneApplicationFrame;
                frame.Navigate(new Uri("/Contexts/Review/Mvvm/IssueReviewPivotPage.xaml?key=" + issue.Key, UriKind.Relative));
            }
        }
    }
}
