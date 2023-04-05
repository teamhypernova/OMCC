using CommunityToolkit.Mvvm.ComponentModel;
using OMCCore.Globalization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OMCCore.Core.Tasks
{
    public partial class OTaskView : ObservableObject
    {
        public ObservableCollection<OTaskItem> Items { get; } = new ObservableCollection<OTaskItem>();
        public void AddItem(OTaskItem item)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Items.Add(item);
                RecalcProgress();
            });
        }
        public void AddItems(params OTaskItem[] items)
        {
            foreach(var i in items)
            {
                AddItem(i);
            }
        }
        public bool RemoveItem(OTaskItem item)
        {
            bool result = false;
            Application.Current.Dispatcher.Invoke(() =>
            {
                result = Items.Remove(item);
                RecalcProgress();
            });
            return result;
        }
        [ObservableProperty] Text? title;
        [ObservableProperty] double progress = 0;

        public OTaskView(Text? title)
        {
            Title = title;
        }

        public void RecalcProgress()
        {
            double prog = 0;
            double progm = 0;
            foreach (var item in Items)
            {
                prog += (item.Progress / item.MaxProgress) * item.Weight;
                progm += item.Weight;
            }
            Progress = prog / progm;
            Debug.WriteLine(Progress);
        }
        public void Start()
        {
            foreach(var item in Items)
            {
                Debug.WriteLine(item.Title?.Content ?? "");
            }
            //TODO:Start
        }
    }
}
