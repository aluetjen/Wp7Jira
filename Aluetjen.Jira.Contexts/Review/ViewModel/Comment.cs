using System;

namespace Aluetjen.Jira.Contexts.Review.Mvvm.ViewModel
{
    public class Comment
    {
        public DateTime PostedOn { get; set; }
        public string User { get; set; }
        public string Description { get; set; }
    }
}