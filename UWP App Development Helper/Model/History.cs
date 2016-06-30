using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Xml.Linq;
using PostSharp.Patterns.Recording;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.Model
{
    public interface IHistoryItemTarget
    {
        XElement Serialize();

        void Load(XElement data);

        string SerializationName { get; }
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

            var oc= new ObservableCollection<Type>();
            oc.CollectionChanged += this.OnKnownTargetsChanged;
            this.KnownTargets = oc;
        }

        private void OnKnownTargetsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //TODO: Check if the new type implements a parameterless constructor and the IHistoryItemTarget interface
        }

        public IReadOnlyList<HistoryItem> History { get; }

        public static HistoryProvider Instance { get; } = new HistoryProvider();

        public IList<Type> KnownTargets { get; }

        public void Add(IHistoryItemTarget target)
        {
            this._history.Add(new HistoryItem()
            {
                Target = target,
            });
        }
    }
}
