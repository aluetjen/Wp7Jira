using System;

namespace Aluetjen.Jira.Contexts.Import.ViewModel
{
    public class Profile : IDocument
    {
        public string Key { get; set; }

        public string JiraUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public DateTime LastLogin { get; set; }
    }
}
