using System;
using System.ComponentModel;
using System.Collections.Generic;
using Aluetjen.Jira.Contexts.Tracking.Documents;
using Aluetjen.Jira.Infrastructure;

namespace Aluetjen.Jira.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public IList<Activity> Activities { get; private set; }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        public void LoadData()
        {
            var key = DateTime.UtcNow.Date.ToString("yyMMdd");
            Activities = Config.DocumentStore.Load<RecentActivities>(key).Activities;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}