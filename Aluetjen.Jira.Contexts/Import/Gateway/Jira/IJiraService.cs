using System;

namespace Aluetjen.Jira.Contexts.Import.Gateway.Jira
{
    public interface IJiraService
    {
        void Get<T>(string resource, Action<T> response);
    }
}