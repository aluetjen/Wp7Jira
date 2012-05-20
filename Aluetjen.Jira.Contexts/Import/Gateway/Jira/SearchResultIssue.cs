namespace Aluetjen.Jira.Contexts.Import.Gateway.Jira
{
    public class SearchResultIssue
    {
        public int Id { get; set; }
        public string Self { get; set; }
        public string Key { get; set; }

        public SearchResultIssueFields Fields { get; set; }
    }
}