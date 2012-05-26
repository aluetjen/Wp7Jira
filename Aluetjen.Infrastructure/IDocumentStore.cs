using System.Collections.Generic;

namespace Aluetjen.Infrastructure
{
    public interface IDocumentStore
    {
        bool Exists<T>(string key);
        T Load<T>(string key) where T : IDocument;
        bool TryLoad<T>(string key, out T value) where T : IDocument;
        T LoadOrCreate<T>(string key) where T : IDocument, new();
        void Store<T>(T document) where T : IDocument;
        IEnumerable<T> LoadAll<T>() where T : IDocument;
        void DeleteAll<T>();
        void Delete<T>(string key) where T : IDocument;
        IEnumerable<string> Keys<T>();
    }
}