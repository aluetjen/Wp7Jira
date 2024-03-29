﻿using System;
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
using Aluetjen.Infrastructure;
using Aluetjen.Jira.Contexts;
using Aluetjen.Jira.Contexts.Import.Domain;
using Aluetjen.Jira.Contexts.Settings.Events;
using Aluetjen.Jira.Contexts.Tracking.Documents;
using Aluetjen.Jira.Infrastructure;
using Microsoft.Phone.Controls;

namespace Aluetjen.Jira
{
    public partial class MainPage : PhoneApplicationPage
    {
        public IBus Bus { get; set; }

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            Bus = Config.Container.Resolve<IBus>();

            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }

            DataContext = App.ViewModel;
        }

        private void ApplicationBarIconButton_Refresh(object sender, EventArgs e)
        {
            Bus.Publish(new ResyncCommand());
        }

        private void ApplicationBarIconButton_Settings(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Contexts/Import/Mvvm/SignInPage.xaml", UriKind.Relative));
        }
    }
}