using System.Windows;
using Aluetjen.Jira.Contexts.Import.Domain;
using Aluetjen.Jira.Contexts.Import.Mvvm.ViewModel;
using Aluetjen.Jira.Infrastructure;
using Microsoft.Phone.Controls;

namespace Aluetjen.Jira.Contexts.Import.Mvvm
{
    public partial class SignInPage : PhoneApplicationPage
    {
        public IDocumentStore Store { get; set; }
        public IBus Bus { get; set; }

        public SignInPage()
        {
            InitializeComponent();

            Store = Config.Container.Resolve<IDocumentStore>();
            Bus = Config.Container.Resolve<IBus>();

            if(Store.Exists<Profile>("Profile"))
            {
                var profile = Store.Load<Profile>("Profile");

                JiraUrl.Text = profile.JiraUrl;
                UserName.Text = profile.UserName;
                Password.Password = profile.Password;
            }
            else
            {
                JiraUrl.Text = "http://localhost:8080";
                UserName.Text = "aluetjen";
                Password.Password = "aluetjen";
                JiraUrl.Focus();
            }
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            Bus.Publish(new SignInCommand
                            {
                                Password = Password.Password,
                                Url = JiraUrl.Text,
                                UserName = UserName.Text
                            });

            NavigationService.GoBack();
        }
    }
}