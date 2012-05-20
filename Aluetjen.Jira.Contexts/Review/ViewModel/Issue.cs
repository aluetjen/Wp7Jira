using System.Collections.Generic;

namespace Aluetjen.Jira.Contexts.Review.ViewModel
{
    public class Issue : IDocument
    {
        public Issue()
        {
            Comments = new List<Comment>();
        }

        public string Key { get; set; }

        public string Summary { get; set; }
        public string Description { get; set; }
        public IList<Comment> Comments { get; set; }
        public string GivenWhenThen { get; set; }
    }
}
