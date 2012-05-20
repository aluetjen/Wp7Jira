using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using Aluetjen.Jira.Contexts;
using Newtonsoft.Json;

namespace Aluetjen.Jira.Infrastructure
{
    public class DocumentStore : IDocumentStore
    {
        // Obtain the virtual store for the application.
        readonly IsolatedStorageFile _myStore = IsolatedStorageFile.GetUserStoreForApplication();

        public bool Exists<T>(string key)
        {
            string dir = typeof(T).ToString();
            string path = Path.Combine(dir, key);

            return _myStore.FileExists(path);
        }

        public IEnumerable<T> LoadAll<T>() where T : IDocument
        {
            return Keys<T>().Select(Load<T>);
        }

        public IEnumerable<string> Keys<T>()
        {
            string path = typeof(T).ToString();
            string searchPattern = string.Format("{0}\\*.*", path);

            if (!_myStore.DirectoryExists(path)) return Enumerable.Empty<string>();

            return _myStore.GetFileNames(searchPattern);
        }

        public void DeleteAll<T>()
        {
            _myStore.DeleteDirectory(typeof (T).ToString());
        }

        public void Delete<T>(string key) where T : IDocument
        {
            var path = GetPathFromKey<T>(key);
            _myStore.DeleteFile(path);
        }

        public T Load<T>(string key) where T : IDocument
        {
            var path = GetPathFromKey<T>(key);

            return LoadFromFile<T>(path);
        }

        public bool TryLoad<T>(string key, out T value) where T : IDocument
        {
            value = default(T);
            if (!Exists<T>(key)) return false;

            value = Load<T>(key);
            return true;
        }

        private static string GetPathFromKey<T>(string key) where T : IDocument
        {
            string dir = typeof (T).ToString();
            string path = Path.Combine(dir, key);
            return path;
        }

        private T LoadFromFile<T>(string path) where T : IDocument
        {
            using (var isoFileStream = new IsolatedStorageFileStream(path, FileMode.Open, _myStore))
            {
                //Write the data
                using (var isoFileWriter = new JsonTextReader(new StreamReader(isoFileStream)))
                {
                    var json = new JsonSerializer();
                    return json.Deserialize<T>(isoFileWriter);
                }
            }
        }

        public void Store<T>(T document) where T : IDocument
        {
            string dir = typeof (T).ToString();
            string path = Path.Combine(dir, document.Key);

            _myStore.CreateDirectory(dir);

            // Specify the file path and options.
            using (var isoFileStream = new IsolatedStorageFileStream(path, FileMode.OpenOrCreate, _myStore))
            {
                //Write the data
                using (var isoFileWriter = new StreamWriter(isoFileStream))
                {
                    var json = new JsonSerializer();
                    json.Serialize(isoFileWriter, document);
                }
            }
        }
    }
}