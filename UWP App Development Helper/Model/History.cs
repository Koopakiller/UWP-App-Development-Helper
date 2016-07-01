using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Storage;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.Model
{
    public interface IHistoryItemTarget
    {
        XElement Serialize();

        void Load(XElement data);
    }

    public class HistoryItem
    {
        public IHistoryItemTarget Target { get; set; }
    }

    public class HistoryProvider
    {
        private readonly List<HistoryItem> _history;

        private HistoryProvider()
        {
            this.History = this._history = new List<HistoryItem>();
        }

        public IReadOnlyList<HistoryItem> History { get; }

        public static HistoryProvider Instance { get; } = new HistoryProvider();

        public Dictionary<string, Type> KnownTargets { get; }=new Dictionary<string, Type>();

        public void Add(IHistoryItemTarget target)
        {
            this._history.Add(new HistoryItem()
            {
                Target = target,
            });
        }

        public async Task LoadAsync(IStorageFile file)
        {
            XDocument doc;
            using (var stream = await file.OpenStreamForReadAsync())
            {
                doc = XDocument.Load(stream);
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
                    var instance = Activator.CreateInstance(type) as IHistoryItemTarget;
                    if (instance == null)
                    {
                        throw new InvalidOperationException($"The type '{type}' does not implement '{nameof(IHistoryItemTarget)}'");
                    }
                    instance.Load(element);
                    this._history.Add(new HistoryItem() {Target = instance});
                }
            }
        }

        public async Task SaveAsync(IStorageFile file)
        {
            var doc = new XDocument("History");
            foreach (var historyItem in this.History)
            {
                doc.Add(historyItem.Target.Serialize());
            }
            using (var stream = await file.OpenStreamForWriteAsync())
            {
                doc.Save(stream);
            }
        }
    }
}
