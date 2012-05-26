using System;
using Aluetjen.Infrastructure;
using Aluetjen.Jira.Contexts.Import.Events;

namespace Aluetjen.Jira.Contexts.Import.ViewModel
{
    public class ProfileLoggedInEventHandler : IHandleMessages<LoggedInEvent>
    {
        public IDocumentStore Store { get; set; }

        public void Handle(LoggedInEvent message)
        {
            Profile profile;
            if(!Store.Exists<Profile>("Profile"))
            {
                profile = new Profile
                              {
                                  Key = "Profile"
                              };
            }
            else
            {
                profile = Store.Load<Profile>("Profile");
            }

            profile.JiraUrl = message.Url;
            profile.LastLogin = DateTime.UtcNow;
            profile.Password = message.Password;
            profile.UserName = message.UserName;

            Store.Store(profile);
        }
    }
}
