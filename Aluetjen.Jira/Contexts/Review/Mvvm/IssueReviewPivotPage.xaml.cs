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
using Aluetjen.Jira.Contexts.Review.Query;
using Aluetjen.Jira.Contexts.Review.ViewModel;
using Aluetjen.Jira.Infrastructure;
using Microsoft.Phone.Controls;

namespace Aluetjen.Jira
{
    public partial class IssueReviewPivotPage : PhoneApplicationPage
    {
        public IssueReviewPivotPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            DataContext = Config.DocumentStore.Load<Issue>(NavigationContext.QueryString["key"]);
            
            base.OnNavigatedTo(e);
        }
    }
}