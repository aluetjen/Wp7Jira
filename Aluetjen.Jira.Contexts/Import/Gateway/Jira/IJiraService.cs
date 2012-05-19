using System;

namespace Aluetjen.Jira.Contexts.Import.Gateway
{
    public interface IJiraService
    {
        void Get<T>(string resource, Action<T> response);
    }
}