using System.Collections.Generic;

namespace Aluetjen.Jira.Contexts
{
    public interface IDocumentStore
    {
        bool Exists<T>(string key);
        T Load<T>(string key) where T : IDocument;
        void Store<T>(T document) where T : IDocument;
        IEnumerable<T> LoadAll<T>() where T : IDocument;
        void DeleteAll<T>();
        void Delete<T>(string key) where T : IDocument;
    }
}