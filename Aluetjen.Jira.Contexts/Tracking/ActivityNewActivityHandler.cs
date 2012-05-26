using Aluetjen.Infrastructure;
using Aluetjen.Jira.Contexts.Tracking.Documents;
using Aluetjen.Jira.Contexts.Tracking.Events;

namespace Aluetjen.Jira.Contexts.Tracking
{
    public class ActivityNewActivityHandler : Handler, IHandleMessages<NewActivityEvent>
    {
        public DocumentStore DocumentStore { get; set; }

        public void Handle(NewActivityEvent message)
        {
            var doc = new Activity
                          {
                              Key = message.Key,
                              Description = message.Description,
                              PostedOn = message.PostedOn
                          };

            DocumentStore.Store(doc);

            RecentActivities recent;
            string key = message.PostedOn.Date.ToString("yyMMdd");
            if (DocumentStore.Exists<RecentActivities>(key))
            {
                recent = DocumentStore.Load<RecentActivities>(key);
            }
            else
            {
                recent = new RecentActivities { Key = key };
            }

            recent.Activities.Add(doc);

            DocumentStore.Store(recent);
        }
    }
}
