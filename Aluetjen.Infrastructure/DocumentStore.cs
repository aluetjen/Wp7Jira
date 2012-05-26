using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using Newtonsoft.Json;

namespace Aluetjen.Infrastructure
{
    public class DocumentStore : IDocumentStore
    {
        // Obtain the virtual store for the application.
        private readonly IsolatedStorageFile _myStore = IsolatedStorageFile.GetUserStoreForApplication();
        private readonly Dictionary<string, WeakReference> _weakReferences = new Dictionary<string, WeakReference>();
        private readonly object _sync = new object();

        public object SyncRoot
        {
            get { return _sync; }
        }

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

        public T LoadOrCreate<T>(string key) where T : IDocument, new()
        {
            lock (SyncRoot)
            {
                T document;
                if (!TryLoad(key, out document))
                {
                    document = new T {Key = key};
                }

                return document;
            }
        }

        private static string GetPathFromKey<T>(string key) where T : IDocument
        {
            string dir = typeof (T).ToString();
            string path = Path.Combine(dir, key);
            return path;
        }

        private T LoadFromFile<T>(string path) where T : IDocument
        {
            T document;
            if (TryGetFromCache(path, out document)) return document;

            lock (SyncRoot)
            {
                using (var isoFileStream = new IsolatedStorageFileStream(path, FileMode.Open, _myStore))
                {
                    //Write the data
                    using (var isoFileWriter = new JsonTextReader(new StreamReader(isoFileStream)))
                    {
                        var json = new JsonSerializer();
                        document = json.Deserialize<T>(isoFileWriter);

                        Cache(path, document);

                        return document;
                    }
                }
            }
        }

        private bool TryGetFromCache<T>(string path, out T document) where T : IDocument
        {
            document = default(T);

            lock (_weakReferences)
            {
                WeakReference weakReference;
                if (_weakReferences.TryGetValue(path, out weakReference))
                {
                    if (weakReference.IsAlive)
                    {
                        document = (T) weakReference.Target;
                        return true;
                    }
                }
            }

            return false;
        }

        private void Cache<T>(string path, T document) where T : IDocument
        {
            lock (_weakReferences)
            {
                _weakReferences[path] = new WeakReference(document);
            }
        }

        public void Store<T>(T document) where T : IDocument
        {
            lock (SyncRoot)
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

                Cache(path, document);
            }
        }
    }
}