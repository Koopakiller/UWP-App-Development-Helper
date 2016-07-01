using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Storage;
using Koopakiller.Apps.UwpAppDevelopmentHelper.ViewModel;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.Model
{
    public interface IHistoryItemTarget
    {
        XElement Serialize();

        void Load(XElement data);

        ViewModelBase PreviewViewModel { get; }
    }

    public class HistoryProvider
    {
        private readonly List<IHistoryItemTarget> _history;

        private HistoryProvider()
        {
            this.History = this._history = new List<IHistoryItemTarget>();
        }

        public IReadOnlyList<IHistoryItemTarget> History { get; }

        public static HistoryProvider Instance { get; } = new HistoryProvider();

        public Dictionary<string, Type> KnownTargets { get; } = new Dictionary<string, Type>();

        public void Add(IHistoryItemTarget target)
        {
            this._history.Insert(0, target);
            while (this._history.Count > 30)
            {
                this._history.RemoveAt(this._history.Count - 1);
            }
        }

        public async Task LoadAsync(IStorageFile file)
        {
            XDocument doc;
            using (var stream = await file.OpenStreamForReadAsync())
            {
                try
                {
                    doc = XDocument.Load(stream);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("An error occurred during loading the history file.");
                    Debug.WriteLine(ex);
                    return;
                }
            }
            foreach (var element in doc.Root.Elements())
            {
                Type type;
                if (!this.KnownTargets.TryGetValue(element.Name.ToString(), out type))
                {
#if DEBUG
                    throw new InvalidOperationException($"Invalid node '{element.Name}' in history");
#elif RELEASE
                    continue;
#else
#error Unknown Konfiguration
#endif
                }
                else
                {
                    try
                    {
                        var instance = Activator.CreateInstance(type) as IHistoryItemTarget;
                        if (instance == null)
                        {
                            throw new InvalidOperationException($"The type '{type}' does not implement '{nameof(IHistoryItemTarget)}'");
                        }
                        instance.Load(element);
                        this._history.Add(instance);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("An error occurred during loading an history entry.");
                        Debug.WriteLine(ex);
                    }
                }
            }
        }

        public async Task SaveAsync(IStorageFile file)
        {
            var doc = new XDocument(new XElement("History"));
            foreach (var historyItem in this.History)
            {
                doc.Root.Add(historyItem.Serialize());
            }
            using (var stream = await file.OpenStreamForWriteAsync())
            {
                doc.Save(stream);
            }
        }
    }
}
